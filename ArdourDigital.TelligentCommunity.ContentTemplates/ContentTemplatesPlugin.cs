using System;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Controls;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.ContentTemplates
{
    public class ContentTemplatesPlugin : IPlugin, IConfigurablePlugin, ITranslatablePlugin
    {
        private ITranslatablePluginController _translationController;

        public string Name => "Ardour Digital - Content Templates";

        public string Description => "Allows defined content templates to be inserted using the rich text editor";

        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                var group = new PropertyGroup("configuration", _translationController.GetLanguageResourceValue("configuration"), 0);

                var property = new Property("template", _translationController.GetLanguageResourceValue("template"), PropertyType.Html, 0, string.Empty);
                property.ControlType = typeof(HtmlEditorStringControl);

                group.Properties.Add(property);

                return new[] { group };
            }
        }

        public Translation[] DefaultTranslations
        {
            get
            {
                var translations = new Translation("en-US");

                translations.Set("configuration", "Configuration");
                translations.Set("template", "Template");

                return new[] { translations };
            }
        }

        public void Initialize()
        {
        }

        public void SetController(ITranslatablePluginController controller)
        {
            _translationController = controller;
        }

        public void Update(IPluginConfiguration configuration)
        {
            ContentTemplateConfiguration.Template = configuration.GetHtml("template");
        }
    }
}
