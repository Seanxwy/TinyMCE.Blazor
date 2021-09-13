// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API
window.TinyMCEBlazor = window.TinyMCEBlazor || {
    // create TinyMCE instance
    createTinyMCE: (domRef, editor, id, value, options, width, height) => {

        function setup(ed) {
            ed.on("init",
                function () {
                    tinymce.get(id).setContent(value);
                    tinymce.execCommand('mceRepaint');
                }
            );

            ed.on("keyup", function () {
                var content = ed.getContent();
                editor.invokeMethodAsync("HandleInput", content)
            });

            ed.on("change", function () {
                var content = ed.getContent();
                editor.invokeMethodAsync("HandleInput", content)
            });

            if (options.toolbar) {
                var toolbarArray = options.toolbar.split(/[| ]/)
                toolbarArray.forEach(btn => {
                    if (btn.indexOf("custom") != -1) {

                        var text = btn
                        var customArray = btn.split("-")
                        if (customArray[1]) {
                            text = customArray[1]
                        }
                        console.log(btn)
                        console.log(text)
                        ed.ui.registry.addButton(btn, {
                            text: text,
                            onAction: () => {
                                editor.invokeMethodAsync("HandleCustomToolbar", btn)
                            }
                        })
                    }
                })
            }
        }



        var config = {
            selector: 'textarea#' + id,
            inline: false,
            plugins: options.plugins,
            toolbar: options.toolbar,
            setup: setup,
            menubar: options.showMenuBar,
            default_link_target: '_blank',
            paste_data_images: options.paste_data_images,
            paste_as_text: options.paste_as_text,
            smart_paste: false,
        };
        if (width) {
            config.width = width
        }
        if (height) {
            config.height = height
        }
        if (options.inlineMode) {
            config.selector = '#' + id;
            config.inline = true;
        }

        tinymce.init(config);
    },
    getValue: (domRef, id) => {
        return tinymce.get(id).getContent({ format: 'text' });
    },
    getHTML: (domRef, id) => {
        return tinymce.get(id).getContent();
    },
    setValue: (domRef, id, value) => {
        if (value == null) {
            value=''
        }
        tinymce.get(id).insertContent(value);
    },
    insertValue: (domRef, id, value) => {
        if (value == null) {
            value = ''
        }
        tinymce.get(id).insertContent(value);
    },
    destroy: (domRef) => {
        tinymce.get(id).destroy();
    },
}