using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyMCE.Blazor.Models
{
    public class TinyMCEOption
    {
        public bool InlineMode { get; set; }

        public string Plugins { get; set; } = "paste autolink link wordcount";

        public string Toolbar { get; set; } = "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | outdent indent | link";

        public bool ShowMenuBar { get; set; } = false;

        public bool PasteAsText { get; set; }

        public bool PasteDataImage { get; set; }
    }
}
