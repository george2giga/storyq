﻿namespace StoryQ.Converter.Wpf
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Xml;
    using System.Xml.Serialization;
    using StoryQ.Converter.Wpf.Properties;
    using StoryQ.Converter.Wpf.ViewModel;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static readonly XmlSerializer settingsSerialiser = new XmlSerializer(typeof(ConversionSettings));

        public Window1()
        {
            this.InitializeComponent();
            ViewModel.Converter vm = (ViewModel.Converter)this.FindResource("vm");
            vm.PlainText = Settings.Default.InputText;

            try
            {
                if (!string.IsNullOrEmpty(Settings.Default.SettingsXml))
                {
                    XmlReader reader = XmlReader.Create(new StringReader(Settings.Default.SettingsXml));
                    if (settingsSerialiser.CanDeserialize(reader))
                    {
                        vm.Settings = (ConversionSettings)settingsSerialiser.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void FocusLastChar(object sender, EventArgs e)
        {
            this.src.Focus();
            this.src.CaretIndex = this.src.Text.Length;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel.Converter vm = (ViewModel.Converter)this.FindResource("vm");
            Settings.Default.InputText = vm.PlainText;
            Settings.Default.SettingsXml = SettingsAsXml(vm.Settings);
            Settings.Default.Save();
        }

        private static string SettingsAsXml(ConversionSettings settings)
        {
            StringWriter writer = new StringWriter();
            var writerSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            settingsSerialiser.Serialize(XmlWriter.Create(writer, writerSettings), settings);
            return writer.ToString();
        }
    }
}
