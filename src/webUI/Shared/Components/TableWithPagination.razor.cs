using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Redpier.Shared.Models;
using Redpier.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Shared.Components
{
    public partial class TableWithPagination<TItem> : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [CascadingParameter]
        public List<TItem> SelectedItems { get; set; }

        [Parameter]
        public int PageSize { get; set; } = 10;

        [Parameter]
        public string ControllerName { get; set; } = typeof(TItem).Name;

        [Parameter]
        public RenderFragment TableHeader { get; set; }

        [Parameter]
        public RenderFragment<TItem> TableRow { get; set; }

        [Parameter]
        public bool IncludeRemoveButton { get; set; } = true;

        [Parameter]
        public EventCallback<List<TItem>> OnItemSelected { get; set; }

        [Parameter]
        public int ColSpan { get; set; }

        public Redpier.Shared.Models.PaginatedList<TItem> Page { get; set; }

        public bool IsBusy { get; set; } = true;


        protected override async Task OnInitializedAsync()
        {
            await FetchNewPageAsync(1);
        }

        public async Task FetchNewPageAsync(int pageNumber)
        {
            try
            {
                IsBusy = true;
                var response = await HttpClient.GetAsync($"/api/{ControllerName}?pagesize={PageSize}&pagenumber={pageNumber}");

                if (response.IsSuccessStatusCode)
                {
                    Page = await response.Content.ReadFromJsonAsync<Redpier.Shared.Models.PaginatedList<TItem>>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                await OnItemSelected.InvokeAsync(new List<TItem>());
                IsBusy = false;
            }
        }

        public async Task RemoveAsync()
        {
            IsBusy = true;
            try
            {
                var responses = new List<HttpResponseMessage>();
                var removedItems = new List<TItem>();
                foreach (var item in SelectedItems)
                {
                    var response = await HttpClient.DeleteAsync($"/api/{ControllerName}?Id={(item as ViewModelBase).Id}");
                    responses.Add(response);
                    if (response.IsSuccessStatusCode)
                        removedItems.Add(item);
                }
                if (responses.Any(r => r.IsSuccessStatusCode))
                {
                    Page.Items.RemoveAll(i => SelectedItems.Contains(i));
                    foreach (var item in removedItems)
                    {
                        SelectedItems.Remove(item);
                    }
                    ToastService.ShowSuccess($"Removed {responses.Where(r => r.IsSuccessStatusCode).Count()} item(s).");
                }
                if (responses.Any(r => !r.IsSuccessStatusCode))
                    ToastService.ShowError($"Failed when removing {responses.Where(r => !r.IsSuccessStatusCode).Count()} item(s).");

            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
            finally
            {
                await OnItemSelected.InvokeAsync(SelectedItems);
                IsBusy = false;
            }
        }

        public async Task SelectAllAsync()
        {
            if (SelectedItems.Any())
                await OnItemSelected.InvokeAsync(new List<TItem>());
            else
                await OnItemSelected.InvokeAsync(Page.Items.ToList());
        }

        public async Task SelectAsync(TItem item)
        {
            if (SelectedItems.Contains(item))
            {
                SelectedItems.Remove(item);
                await OnItemSelected.InvokeAsync(SelectedItems);
            }
            else
            {
                SelectedItems.Add(item);
                await OnItemSelected.InvokeAsync(SelectedItems);
            }
        }
    }
}
