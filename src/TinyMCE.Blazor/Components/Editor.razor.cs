using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using TinyMCE.Blazor.Models;

namespace TinyMCE.Blazor
{
    public partial class Editor : ComponentBase, IDisposable
    {
        /// <summary>
        /// Markdown Content
        /// </summary>
        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _waitingUpdate = true;
                }
            }
        }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        /// <summary>
        /// Html Content
        /// </summary>
        [Parameter]
        public string Html { get; set; }

        [Parameter]
        public string Id { get; set; } = null;

        [Parameter] public EventCallback<string> HtmlChanged { get; set; }

        [Parameter]
        public TinyMCEOption TinyMCEOption { get; set; } = new TinyMCEOption();


        [Parameter]
        public string Height {
            get => _heightValue; 
            set {
                SetHeight(value);
                _heightValue = value;
            }
        }

        [Parameter]
        public string Width { get; set; }

        private ElementReference _ref;

        private bool _editorRendered = false;
        private bool _waitingUpdate = false;
        private string _value, _heightValue = "500";

        private bool _afterFirstRender = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                //Console.WriteLine("edit is first render");
                await CreateTinyMCE();
            }
            
        }

        protected override async Task OnInitializedAsync()
        {
            //Console.WriteLine("edit is initialized");
            await base.OnInitializedAsync();
            this.Id = Id ?? Guid.NewGuid().ToString();
        }

        protected override async Task OnParametersSetAsync()
        {
            //Console.WriteLine("edit is on parameters");
            await base.OnParametersSetAsync();
            if (_waitingUpdate && _editorRendered)
            {
                _waitingUpdate = false;
                await SetValue(_value);
                //Console.WriteLine("edit is on parameters set value");
            }
        }

        public async ValueTask<string> GetValueAsync()
        {
            if (_editorRendered)
            {
                return await GetValue();
            }

            return string.Empty;
        }

        public void Dispose()
        {
            //Console.WriteLine("edit is dispose");
            Destroy();
        }
    }
}
