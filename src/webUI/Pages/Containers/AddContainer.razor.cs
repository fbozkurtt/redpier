using Redpier.Web.UI.Shared.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components;
using Redpier.Web.UI.Interfaces;
using Redpier.Web.UI.Helpers;
using Redpier.Web.UI.Models;
using Blazored.LocalStorage;

namespace Redpier.Web.UI.Pages.Containers
{
    public partial class AddContainer
    {
        [Inject]
        public IImageService ImageService { get; set; }

        [Inject]
        public IContainerService ContainerService { get; set; }

        [Inject]
        public INetworkService NetworkService { get; set; }

        [Inject]
        public IVolumeService VolumeService { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        CreateContainerParameters Model { get; set; } = new CreateContainerParameters()
        {
            Env = new List<string>(),
            Cmd = null,
            ExposedPorts = new Dictionary<string, EmptyStruct>(),
            Entrypoint = null,
            //Volumes = new Dictionary<string, EmptyStruct>(),
            Labels = new Dictionary<string, string>(),
            HostConfig = new HostConfig()
            {
                RestartPolicy = new RestartPolicy() { Name = RestartPolicyKind.No },
                PortBindings = new Dictionary<string, IList<PortBinding>>(),
                PublishAllPorts = false,
                //Binds = new List<string>(),
                Mounts = new List<Mount>(),
                AutoRemove = false,
                NetworkMode = "bridge",
                Privileged = false,
                Init = false,
                Runtime = null,
                ExtraHosts = new List<string>(),
                Devices = new List<DeviceMapping>(),
                //Memory = 1024 * 1024 * 1024,
                //MemoryReservation = 1024 * 1024 * 1024,
                //NanoCPUs = 2 * 1000000000,
                //CapAdd = new List<string>()
                //{
                //    "SETPCAP",
                //    "MKNOD",
                //    "AUDIT_WRITE",
                //    "CHOWN",
                //    "NET_RAW",
                //    "DAC_OVERRIDE",
                //    "FOWNER",
                //    "FSETID",
                //    "KILL",
                //    "SETGID",
                //    "SETUID",
                //    "NET_BIND_SERVICE",
                //    "SYS_CHROOT",
                //    "SETFCAP",
                //},
                //CapDrop = new List<string>()
                //{
                //    "SYS_MODULE",
                //    "SYS_RAWIO",
                //    "SYS_PACCT",
                //    "SYS_ADMIN",
                //    "SYS_NICE",
                //    "SYS_RESOURCE",
                //    "SYS_TIME",
                //    "SYS_TTY_CONFIG",
                //    "AUDIT_CONTROL",
                //    "MAC_ADMIN",
                //    "MAC_OVERRIDE",
                //    "NET_ADMIN",
                //    "SYSLOG",
                //    "DAC_READ_SEARCH",
                //    "LINUX_IMMUTABLE",
                //    "NET_BROADCAST",
                //    "IPC_LOCK",
                //    "IPC_OWNER",
                //    "SYS_PTRACE",
                //    "SYS_BOOT",
                //    "LEASE",
                //    "WAKE_ALARM",
                //    "BLOCK_SUSPEND"
                //},
                Sysctls = new Dictionary<string, string>()
            },
            NetworkingConfig = new NetworkingConfig()
            {
                EndpointsConfig = new Dictionary<string, EndpointSettings>()
                {
                    { "bridge", new EndpointSettings() {  IPAMConfig = new EndpointIPAMConfig() } },
                }
            },
            OpenStdin = false,
            Tty = false
        };

        private List<string> LocalImages { get; set; } = new List<string>();

        private List<string> Networks { get; set; } = new List<string>();

        private List<string> ExistingVolumes { get; set; } = new List<string>();

        private bool OverrideDefaultCommand { get; set; } = false;

        private bool OverrideDefaultEntryPoint { get; set; } = false;

        private string Command { get; set; }

        private string Entrypoint { get; set; }

        private bool PullImage { get; set; } = false;

        private int MemoryLimit { get; set; } = 0;

        private long CpuLimit { get; set; } = 0;

        private string NetworkMode
        {
            get
            {
                return Model.HostConfig.NetworkMode;
            }
            set
            {
                ResetNetworkConfig();
                Model.HostConfig.NetworkMode = value;
                Model.NetworkingConfig.EndpointsConfig.Add(value, new EndpointSettings() { IPAMConfig = new EndpointIPAMConfig() });
            }
        }

        private string FirstDns { get; set; }

        private string SecondDns { get; set; }

        private List<Mount> Binds { get; set; } = new List<Mount>();

        private List<Mount> Volumes { get; set; } = new List<Mount>();

        private List<Label> Labels = new List<Label>();

        private string LabelName;

        private string LabelValue;

        private ConsoleType Console { get; set; } = ConsoleType.None;

        private SystemInfoResponse SystemInfo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var images = await ImageService.GetAllAsync().ConfigureAwait(false);
            LocalImages = images.Select(i => i.RepoTags).Aggregate((a, b) => a.Concat(b).ToList()).ToList();

            var networks = await NetworkService.GetAllAsync().ConfigureAwait(false);
            Networks = networks.Select(n => n.Name).ToList();

            var volumes = await VolumeService.GetAllAsync().ConfigureAwait(false);
            ExistingVolumes = volumes.Select(v => v.Name).ToList();

            SystemInfo = await LocalStorage.GetItemAsync<SystemInfoResponse>("systemInfo");

            PageLoaded = true;
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                IsBusy = true;

                if (PullImage)
                {
                    ToastService.ShowInfo($"Pulling image");
                    var pullResult = await ImageService.Pull(Model.Image);
                    if (pullResult)
                        ToastService.ShowSuccess($"Image pulled");
                }

                if (OverrideDefaultCommand && !string.IsNullOrWhiteSpace(Command))
                    Model.Cmd = ContainerHelper.CommandStringToArray(Command);

                if (OverrideDefaultEntryPoint && !string.IsNullOrWhiteSpace(Command) && !string.IsNullOrWhiteSpace(Entrypoint))
                    Model.Entrypoint = new List<string>() { Entrypoint };

                if (MemoryLimit > 0)
                    Model.HostConfig.Memory = MemoryLimit * 1024 * 1024;

                if (CpuLimit > 0)
                    Model.HostConfig.NanoCPUs = CpuLimit * 1000000000;

                if (!string.IsNullOrWhiteSpace(FirstDns))
                    Model.HostConfig.DNS.Add(FirstDns);

                if (!string.IsNullOrWhiteSpace(SecondDns))
                    Model.HostConfig.DNS.Add(SecondDns);

                if (Volumes.Count > 0)
                    Model.HostConfig.Mounts = Model.HostConfig.Mounts.Concat(Volumes).ToList();

                if (Binds.Count > 0)
                    Model.HostConfig.Mounts = Model.HostConfig.Mounts.Concat(Binds).ToList();

                if (Labels.Count > 0)
                {
                    foreach (var label in Labels)
                    {
                        Model.Labels.Add(label.Name, label.Value);
                    }
                }

                SetConsole();

                var result = await ContainerService.CreateAsync(Model);

                if (result.ID != null)
                    ToastService.ShowSuccess($"Created container {result.ID}");

                if (result.Warnings.Count > 0)
                    ToastService.ShowWarning($"{result.Warnings.Aggregate((a, b) => a + "\n" + b)}");
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"An error occured while creating container {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddVolume()
        {
            Volumes.Add(new Mount() { Type = "volume" });
        }

        private void AddBindMount()
        {
            Binds.Add(new Mount() { Type = "bind" });
        }

        private void AddLabel()
        {
            if (string.IsNullOrWhiteSpace(LabelValue) || string.IsNullOrWhiteSpace(LabelName))
                return;
            Labels.Add(new Label() { Name = LabelName, Value = LabelValue });
            LabelName = string.Empty;
            LabelValue = string.Empty;
        }

        private void RemoveLabel(Label label)
        {
            Labels.Remove(label);
        }

        private void RemoveVolume(Mount bind)
        {
            Volumes.Remove(bind);
        }

        private void RemoveBind(Mount bind)
        {
            Binds.Remove(bind);
        }

        private void ResetNetworkConfig()
        {
            Model.NetworkingConfig = new NetworkingConfig()
            {
                EndpointsConfig = new Dictionary<string, EndpointSettings>()
            };
        }

        private void SetConsole()
        {
            var openStdin = true;
            var tty = true;
            if (Console.Equals(ConsoleType.Tty))
            {
                openStdin = false;
            }
            else if (Console.Equals(ConsoleType.Interactive))
            {
                tty = false;
            }
            else if (Console.Equals(ConsoleType.None))
            {
                openStdin = false;
                tty = false;
            }
            Model.OpenStdin = openStdin;
            Model.Tty = tty;
        }
        private async Task<IEnumerable<string>> SearchImages(string searchText)
        {
            return await Task.FromResult(LocalImages.Where(i => i.ToLowerInvariant().Contains(searchText.ToLowerInvariant())).ToList());
        }

        private class Label
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
