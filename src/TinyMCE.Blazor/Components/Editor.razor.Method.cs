using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TinyMCE.Blazor
{
    public partial class Editor : ComponentBase
    {
        [Inject] private IJSRuntime Js { get; set; }

        public async Task CreateTinyMCE()
        {
            await Js.InvokeVoidAsync("TinyMCEBlazor.createTinyMCE", _ref, DotNetObjectReference.Create(this), this.Id, this.Value, this.TinyMCEOption, this.Width, this.Height);
        }

        public ValueTask<string> GetValue()
        {
            return Js.InvokeAsync<string>("TinyMCEBlazor.getValue", _ref, this.Id);
        }

        public ValueTask<string> GetHTML()
        {
            return Js.InvokeAsync<string>("TinyMCEBlazor.getHTML", _ref, this.Id);
        }

        public async Task SetValue(string value)
        {
            await Js.InvokeVoidAsync("TinyMCEBlazor.setValue", _ref, this.Id, value);
        }

        public async Task InsertValue(string value)
        {
            await Js.InvokeVoidAsync("TinyMCEBlazor.insertValue", _ref, this.Id, value);
        }

        public async Task Destroy()
        {
            await Js.InvokeVoidAsync("TinyMCEBlazor.destroy", _ref);
        }

        public async Task SetHeight(string value, bool stop = false)
        {
            await Js.InvokeVoidAsync("TinyMCEBlazor.setHeight", _ref, value, stop);
        }
    }
}