using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Controls;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.ContentTemplates
{
    public class ContentTemplatesPlugin : IPlugin, IConfigurablePlugin
    {
        public string Name => "Ardour Digital - Content Templates";

        public string Description => "Allows defined content templates to be inserted using the rich text editor";

        public PropertyGroup[] ConfigurationOptions
        {
            get
            {
                var group = new PropertyGroup("configuration", "Configuration", 0);

                var property = new Property("template", "Template", PropertyType.Html, 0, string.Empty);
                property.ControlType = typeof(HtmlEditorStringControl);

                group.Properties.Add(property);

                return new[] { group };
            }
        }

        public void Initialize()
        {
        }

        public void Update(IPluginConfiguration configuration)
        {
            ContentTemplateConfiguration.Template = configuration.GetHtml("template");
        }
    }
}
