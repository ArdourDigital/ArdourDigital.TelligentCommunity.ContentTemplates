using System;
using System.Collections.Generic;
using Telligent.DynamicConfiguration.Components;
using Telligent.Evolution.Controls;
using Telligent.Evolution.Extensibility.EmbeddableContent.Version1;
using Telligent.Evolution.Extensibility.Storage.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.ContentTemplates
{
    public class ContentTemplatesPlugin : IPlugin, IConfigurablePlugin, ITranslatablePlugin, IEmbeddableContentFragmentType
    {
        private ITranslatablePluginController _translationController;
        private readonly Guid _typeId = new Guid("7b78ba19-26b5-4985-94ec-40a42d25caea");

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
                translations.Set("name", "Content Template");
                translations.Set("description", "Insert text from a predefined template");

                return new[] { translations };
            }
        }

        public string ContentFragmentName => _translationController.GetLanguageResourceValue("name");

        public string ContentFragmentDescription => _translationController.GetLanguageResourceValue("description");

        public Guid EmbeddedContentFragmentTypeId => _typeId;

        public PropertyGroup[] EmbedConfiguration
        {
            get
            {
                var group = new PropertyGroup("configuration", _translationController.GetLanguageResourceValue("configuration"), 0);

                var value = new Property("value", _translationController.GetLanguageResourceValue("template"), PropertyType.Html, 0, ContentTemplateConfiguration.Template);
                value.ControlType = typeof(HtmlEditorStringControl);

                group.Properties.Add(value);

                return new[] { group };
            }
        }

        public string PreviewImageUrl => CentralizedFileStorage.GetGenericDownloadUrl("system", "images", "embedcode.png");

        public void AddUpdateContentFragments(Guid contentId, Guid contentTypeId, IEnumerable<IEmbeddableContentFragment> embeddedFragments)
        {
        }

        public bool CanEmbed(Guid contentTypeId, int userId)
        {
            return true;
        }

        public void Initialize()
        {
        }

        public string Render(IEmbeddableContentFragment embeddedFragment, string target)
        {
            return embeddedFragment.GetHtml("value");
        }

        public void SetController(ITranslatablePluginController controller)
        {
            _translationController = controller;
        }

        public void Update(IPluginConfiguration configuration)
        {
            ContentTemplateConfiguration.Template = configuration.GetHtml("template");
        }

        public EmbeddableContentFragmentValidationState Validate(IEmbeddableContentFragment embeddedFragment)
        {
            return new EmbeddableContentFragmentValidationState(true);
        }
    }
}
