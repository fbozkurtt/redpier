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
    public partial class DataTableWithPagination<TItem> : ComponentBase
    {
        [Parameter]
        public List<TItem> Items { get; set; }

        [Parameter]
        public List<TItem> SelectedItems { get; set; }

        [Parameter]
        public int PageSize { get; set; } = 10;

        [Parameter]
        public RenderFragment TableHeader { get; set; }

        [Parameter]
        public RenderFragment<TItem> TableRow { get; set; }

        [Parameter]
        public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }

        [Parameter]
        public int ColSpan { get; set; }

        [Parameter]
        public bool IsSmallTable { get; set; } = true;

        [Parameter]
        public bool IsBusy { get; set; }

        public PaginatedList<TItem> Page { get; set; }

        public async Task RefreshContent()
        {
            await SetPageAsync(Page?.PageIndex ?? 1);
            //StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPageAsync(1);
            //StateHasChanged();
        }

        public async Task SetPageAsync(int pageNumber)
        {
            Page = Items.AsQueryable().ToPaginatedList(pageNumber, PageSize);
            //if (pageNumber != Page.PageIndex)
            SelectedItems.Clear();

            await SelectedItemsChanged.InvokeAsync(SelectedItems);
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