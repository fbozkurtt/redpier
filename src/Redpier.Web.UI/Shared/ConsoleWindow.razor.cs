using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Shared
{
    public partial class ConsoleWindow : ComponentBase
    {

        //    private IList<ConsoleLine> consoleLines;

        //    private int lineId = 0;

        //    public ConsoleWindow()
        //    {
        //        consoleLines = new List<ConsoleLine>() { new ConsoleLine(lineId) };
        //    }

        //    [Parameter]
        //    public string Id { get; set; }

        //    public void KeyPress(KeyboardEventArgs e)
        //    {
        //        if (e.Key.Equals("Enter"))
        //        {
        //            Console.WriteLine(lineId);
        //            consoleLines[lineId].IsDisabled = true;
        //            consoleLines.Add(new ConsoleLine(++lineId));
        //            Console.WriteLine(lineId);
        //        }
        //    }

        //    protected override async Task OnInitializedAsync()
        //    {
        //        await consoleLines[lineId].ElementReference.FocusAsync();
        //    }
        //}

        //public class ConsoleLine
        //{
        //    public readonly int Index;

        //    public ElementReference ElementReference;

        //    public bool IsDisabled { get; set; } = false;

        //    public ConsoleLine(int index)
        //    {
        //        this.Index = index;
        //    }
        //}
    }
}
