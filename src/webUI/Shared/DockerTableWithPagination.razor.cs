using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Redpier.Shared.Extensions;
using Redpier.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Shared
{
    public partial class DockerTableWithPagination<TItem> : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Parameter]
        public List<TItem> Items { get; set; }

        [Parameter]
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
        public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }

        [Parameter]
        public int ColSpan { get; set; }

        public PaginatedList<TItem> Page { get; set; }

        public bool IsBusy { get; set; } = true;

        public double ProgressBarValue { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await FetchDataAsync();
            await FetchNewPageAsync(1);
        }
        public async Task FetchDataAsync()
        {
            try
            {
                IsBusy = true;
                var response = await HttpClient.GetAsync($"/api/{ControllerName}");

                if (response.IsSuccessStatusCode)
                {
                    Items = await response.Content.ReadFromJsonAsync<List<TItem>>();
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
                IsBusy = false;
            }
        }

        public async Task FetchNewPageAsync(int pageNumber)
        {
            Page = Items.AsQueryable().ToPaginatedList(pageNumber, PageSize);
            SelectedItems.Clear();

            await SelectedItemsChanged.InvokeAsync(SelectedItems);
        }

        public async Task RemoveAsync()
        {
            IsBusy = true;
            try
            {
                ProgressBarValue = 1;
                var responses = new List<HttpResponseMessage>();
                var removedItems = new List<TItem>();
                foreach (var item in SelectedItems)
                {
                    var response = await HttpClient.DeleteAsync($"/api/{ControllerName}?Id={item.GetType().GetProperty("ID").GetValue(item)}");
                    responses.Add(response);
                    if (response.IsSuccessStatusCode)
                        removedItems.Add(item);
                    ProgressBarValue += 1;
                    StateHasChanged();
                }
                if (responses.Any(r => r.IsSuccessStatusCode))
                {
                    Page.Items.RemoveAll(i => SelectedItems.Contains(i));
                    Items.RemoveAll(i => SelectedItems.Contains(i));
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
                await SelectedItemsChanged.InvokeAsync(SelectedItems);
                IsBusy = false;
            }
        }

        public async Task SelectAllAsync()
        {
            if (SelectedItems.Any())
                SelectedItems.Clear();
            else
                SelectedItems = Page.Items.ToList();

            await SelectedItemsChanged.InvokeAsync(SelectedItems);
        }

        public async Task SelectAsync(TItem item)
        {
            if (SelectedItems.Contains(item))
                SelectedItems.Remove(item);
            else
                SelectedItems.Add(item);

            await SelectedItemsChanged.InvokeAsync(SelectedItems);
        }
    }
}