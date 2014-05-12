using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RESTful
{
    class HeaderFields
    {
        public static void AddField()
        {
            // Get the HeadersGrid from the MainWindow
            Grid HeadersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).HeadersGrid;

            // Get Current Header count
            int headerCount = HeadersGrid.RowDefinitions.Count();

            // Add a header/key textbox
            TextBox header = new TextBox();
            header.SetValue(Grid.RowProperty, headerCount - 1);
            header.SetValue(Grid.ColumnProperty, 0);
            header.Name = String.Format("requestHeaderKey{0}", headerCount);
            HeadersGrid.Children.Add(header);

            // Add a value textbox
            TextBox value = new TextBox();
            value.SetValue(Grid.RowProperty, headerCount - 1);
            value.SetValue(Grid.ColumnProperty, 1);
            value.Name = String.Format("requestHeaderValue{0}", headerCount);
            HeadersGrid.Children.Add(value);

            // Add the remove button
            Label removeHeaderButton = new Label();
            removeHeaderButton.SetValue(Grid.RowProperty, headerCount - 1);
            removeHeaderButton.SetValue(Grid.ColumnProperty, 2);
                var main = App.Current.MainWindow as MainWindow;
            removeHeaderButton.MouseDown += main.RemoveHeader_Click;
            removeHeaderButton.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "/Resources/#GLYPHICONS Halflings");
            removeHeaderButton.ToolTip = "Remove Header";
            removeHeaderButton.Foreground = Brushes.DarkRed;
            removeHeaderButton.FontSize = 16;
            removeHeaderButton.Content = "\ue083";
            removeHeaderButton.HorizontalAlignment = HorizontalAlignment.Center;
            HeadersGrid.Children.Add(removeHeaderButton);

            // Add a row to the HeadersGrid
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            HeadersGrid.RowDefinitions.Add(rowDefinition);

            // Get AddHeader element
            Label AddHeader = (Label)HeadersGrid.FindName("AddHeader");

            // Move the Add Header button down a row
            AddHeader.SetValue(Grid.RowProperty, (headerCount + 1));
        }

        public static HeaderDataElement[] GridToHeaderDataElement()
        {
            // Get the HeadersGrid from the MainWindow
            Grid HeadersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).HeadersGrid;

            // Ensure there are fields
            if (HeadersGrid.Children.Count > 3)
            {
                // Create a dictionary to hold values
                Dictionary<string, string> dict = new Dictionary<string, string>();

                // Get each parameter starting after the first 3 UIElements and skipping the last UIElement
                for (int i = 3; i < HeadersGrid.Children.Count - 1; i++)
                {
                    // Create stings for Key and Value of dictionary row
                    string fieldKey = "";
                    string fieldValue = "";

                    // Check if the UIElement is a TextBox
                    if (HeadersGrid.Children.Cast<UIElement>().ElementAt(i).GetType() == typeof(TextBox))
                    {
                        // Cast the child as a TextBox
                        TextBox uie = (TextBox)HeadersGrid.Children.Cast<UIElement>().ElementAt(i);

                        // Ensure the TextBox x:Name contains "Key"
                        if (uie.Name.Contains("Key"))
                        {
                            // Extract the Content value of the Label
                            fieldKey = uie.Text;

                            // Check if the next UIElement is a TextBox
                            if (HeadersGrid.Children.Cast<UIElement>().ElementAt(i + 1).GetType() == typeof(TextBox))
                            {
                                // Cast the child as a TextBox
                                TextBox uie2 = (TextBox)HeadersGrid.Children.Cast<UIElement>().ElementAt(i + 1);

                                // Ensure the TextBox x:Name contains "Value"
                                if (uie2.Name.Contains("Value"))
                                {
                                    // Extract the Text value of the TextBox
                                    fieldValue = uie2.Text;
                                }
                            }
                        }
                    }

                    // Ensure key:value has data
                    if ((fieldKey != "") && (fieldValue != "") && (dict.ContainsKey(fieldKey) == false))
                    {
                        // Add row to dictionary
                        dict.Add(fieldKey, fieldValue);
                    }
                }

                // Add items in dictionary to array
                if (dict.Count > 0)
                {
                    // Create a HeaderDataElement array
                    HeaderDataElement[] hdea = dict.Select(pair => new HeaderDataElement()
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    }).ToArray();

                    return hdea;
                }
                else // No items in dictionary
                {
                    return null;
                }
            }
            else // No fields to save
            {
                return null;
            }
        }

        public static void HeaderDataElementToGrid(Request request)
        {
            // Get the HeadersGrid from the MainWindow
            Grid HeadersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).HeadersGrid;

            // Get a count of key:value pairs to add
            int headersCount = request.Headers.Count();

            // Foreach parameter in request.Headers Add a row and populate the fields
            foreach (HeaderDataElement pde in request.Headers)
            {
                // Genereate header row
                HeaderFields.AddField();

                // Get count of the children
                int count = HeadersGrid.Children.Count;

                // Set the Key
                TextBox key = (TextBox)HeadersGrid.Children.Cast<UIElement>().ElementAt(count - 3);
                key.Text = pde.Key;

                // Set the Value
                TextBox value = (TextBox)HeadersGrid.Children.Cast<UIElement>().ElementAt(count - 2);
                value.Text = pde.Value;
            }

            // Set the HeadersGrid of Main window to the HeadersGrid variable
            ((MainWindow)System.Windows.Application.Current.MainWindow).HeadersGrid = HeadersGrid;
        }
    }
}
