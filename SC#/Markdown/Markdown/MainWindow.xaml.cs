using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Markdig;

namespace Markdown
{
    public partial class MainWindow : Window
    {
        private bool previewVisible = true;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await PreviewBrowser.EnsureCoreWebView2Async(null);
        }

        private void MarkdownInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!previewVisible || PreviewBrowser.CoreWebView2 == null) return;
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            string markdownText = MarkdownInput.Text;
            string html = Markdig.Markdown.ToHtml(markdownText);

            string fullHtml = $@"
<html>
<head>
<meta charset='UTF-8'>
<style>
body {{
  background-color: white;
  color: black;
  font-family: 'Segoe UI', sans-serif;
  margin: 20px;
}}
h1, h2, h3, h4, h5, h6 {{
  color: #2D2D2D;
  border-bottom: 1px solid #e0e0e0;
  padding-bottom: 5px;
}}
pre {{
  background: #F5F5F5;
  padding: 10px;
  border-radius: 6px;
  overflow-x: auto;
}}
code {{ color: #C7254E; font-family: Consolas, monospace; }}
a {{ color: #0078D7; text-decoration: none; }}
a:hover {{ text-decoration: underline; }}
</style>
</head>
<body>{html}</body>
</html>";

            PreviewBrowser.NavigateToString(fullHtml);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Markdown Files|*.md|Text Files|*.txt|All Files|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                MarkdownInput.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Markdown Files|*.md|Text Files|*.txt"
            };

            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, MarkdownInput.Text);
            }
        }

        private void ExportHtml_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "HTML Files|*.html"
            };

            if (dlg.ShowDialog() == true)
            {
                string html = Markdig.Markdown.ToHtml(MarkdownInput.Text);
                string fullHtml = $"<html><head><meta charset='UTF-8'><style>body{{font-family:'Segoe UI';margin:20px;}}</style></head><body>{html}</body></html>";
                File.WriteAllText(dlg.FileName, fullHtml, Encoding.UTF8);
                MessageBox.Show("HTML olarak dışa aktarıldı!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TogglePreview_Click(object sender, RoutedEventArgs e)
        {
            previewVisible = !previewVisible;
            PreviewBrowser.Visibility = previewVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Bold_Click(object sender, RoutedEventArgs e) => InsertMarkdown("**bold**");
        private void Italic_Click(object sender, RoutedEventArgs e) => InsertMarkdown("_italic_");
        private void Heading_Click(object sender, RoutedEventArgs e) => InsertMarkdown("# Başlık");

        private void InsertMarkdown(string text)
        {
            var cursor = MarkdownInput.CaretIndex;
            MarkdownInput.Text = MarkdownInput.Text.Insert(cursor, text);
            MarkdownInput.CaretIndex = cursor + text.Length;
        }

        private void Undo_Click(object sender, RoutedEventArgs e) => MarkdownInput.Undo();
        private void Redo_Click(object sender, RoutedEventArgs e) => MarkdownInput.Redo();
        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
