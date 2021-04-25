using Redpier.Web.UI.Shared.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components;
using Redpier.Web.UI.Interfaces;
using Redpier.Web.UI.Helpers;

namespace Redpier.Web.UI.Pages.Containers
{
    public partial class Add
    {
        [Inject]
        public IImageService ImageService { get; set; }

        [Inject]
        public IContainerService ContainerService { get; set; }

        [Inject]
        public INetworkService NetworkService { get; set; }

        CreateContainerParameters Model { get; set; } = new CreateContainerParameters()
        {
            Env = new List<string>(),
            Cmd = null,
            ExposedPorts = new Dictionary<string, EmptyStruct>(),
            Entrypoint = null,
            HostConfig = new HostConfig()
            {
                RestartPolicy = new RestartPolicy() { Name = RestartPolicyKind.No },
                PortBindings = new Dictionary<string, IList<PortBinding>>(),
                PublishAllPorts = false,
                Binds = new List<string>(),
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
            Labels = new Dictionary<string, string>(),
            OpenStdin = false,
            Tty = false
        };

        public List<string> LocalImages { get; set; } = new List<string>();

        public List<string> Networks { get; set; } = new List<string>();

        public bool OverrideDefaultCommand { get; set; } = false;

        public bool OverrideDefaultEntryPoint { get; set; } = false;

        public string Command { get; set; }

        public string Entrypoint { get; set; }

        public bool PullImage { get; set; } = false;

        public int MemoryLimit { get; set; }

        public long CpuLimit { get; set; } = 0;

        public string NetworkMode
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

        public string FirstDns { get; set; }

        public string SecondDns { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var images = await ImageService.GetAllAsync();
            LocalImages = images.Select(i => i.RepoTags).Aggregate((a, b) => a.Concat(b).ToList()).ToList();

            var networks = await NetworkService.GetAllAsync();
            Networks = networks.Select(n => n.Name).ToList();

            PageLoaded = true;
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                IsBusy = true;

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

        private void ResetNetworkConfig()
        {
            Model.NetworkingConfig = new NetworkingConfig()
            {
                EndpointsConfig = new Dictionary<string, EndpointSettings>()
            };
        }

        private async Task<IEnumerable<string>> SearchImages(string searchText)
        {
            return await Task.FromResult(LocalImages.Where(i => i.ToLowerInvariant().Contains(searchText.ToLowerInvariant())).ToList());
        }
    }
}
