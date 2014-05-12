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
    class ParameterFields
    {
        public static void AddField()
        {
            // Get the ParametersGrid from the MainWindow
            Grid ParametersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).ParametersGrid;

            // Get Current Header count
            int headerCount = ParametersGrid.RowDefinitions.Count();

            // Add a header/key textbox
            TextBox header = new TextBox();
            header.SetValue(Grid.RowProperty, headerCount - 1);
            header.SetValue(Grid.ColumnProperty, 0);
            header.Name = String.Format("requestParameterKey{0}", headerCount);
            ParametersGrid.Children.Add(header);

            // Add a value textbox
            TextBox value = new TextBox();
            value.SetValue(Grid.RowProperty, headerCount - 1);
            value.SetValue(Grid.ColumnProperty, 1);
            value.Name = String.Format("requestParameterValue{0}", headerCount);
            ParametersGrid.Children.Add(value);

            // Add the remove button
            Label removeParameterButton = new Label();
            removeParameterButton.SetValue(Grid.RowProperty, headerCount - 1);
            removeParameterButton.SetValue(Grid.ColumnProperty, 2);
                var main = App.Current.MainWindow as MainWindow;
            removeParameterButton.MouseDown += main.RemoveParameter_Click;
            removeParameterButton.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "/Resources/#GLYPHICONS Halflings");
            removeParameterButton.ToolTip = "Remove Parameter";
            removeParameterButton.Foreground = Brushes.DarkRed;
            removeParameterButton.FontSize = 16;
            removeParameterButton.Content = "\ue083";
            removeParameterButton.HorizontalAlignment = HorizontalAlignment.Center;
            ParametersGrid.Children.Add(removeParameterButton);

            // Add a row to the ParametersGrid
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            ParametersGrid.RowDefinitions.Add(rowDefinition);

            // Get AddParameter element
            Label AddParameter = (Label)ParametersGrid.FindName("AddParameter");

            // Move the Add Header button down a row
            AddParameter.SetValue(Grid.RowProperty, (headerCount + 1));
        }

        public static ParameterDataElement[] GridToParameterDataElement()
        {
            // Get the ParametersGrid from the MainWindow
            Grid ParametersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).ParametersGrid;

            // Ensure there are fields
            if (ParametersGrid.Children.Count > 3)
            {
                // Create a dictionary to hold values
                Dictionary<string, string> dict = new Dictionary<string, string>();

                // Get each parameter starting after the first 3 UIElements and skipping the last UIElement
                for (int i = 3; i < ParametersGrid.Children.Count - 1; i++)
                {
                    // Create stings for Key and Value of dictionary row
                    string fieldKey = "";
                    string fieldValue = "";

                    // Check if the UIElement is a TextBox
                    if (ParametersGrid.Children.Cast<UIElement>().ElementAt(i).GetType() == typeof(TextBox))
                    {
                        // Cast the child as a TextBox
                        TextBox uie = (TextBox)ParametersGrid.Children.Cast<UIElement>().ElementAt(i);

                        // Ensure the TextBox x:Name contains "Key"
                        if (uie.Name.Contains("Key"))
                        {
                            // Extract the Content value of the Label
                            fieldKey = uie.Text;

                            // Check if the next UIElement is a TextBox
                            if (ParametersGrid.Children.Cast<UIElement>().ElementAt(i + 1).GetType() == typeof(TextBox))
                            {
                                // Cast the child as a TextBox
                                TextBox uie2 = (TextBox)ParametersGrid.Children.Cast<UIElement>().ElementAt(i + 1);
                                
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
                    // Create a ParameterDataElement array
                    ParameterDataElement[] pdea = dict.Select(pair => new ParameterDataElement()
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    }).ToArray();

                    return pdea;
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

        public static void ParameterDataElementToGrid(Request request)
        {
            // Get the ParametersGrid from the MainWindow
            Grid ParametersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).ParametersGrid;

            // Get a count of key:value pairs to add
            int parametersCount = request.Parameters.Count();

            // Foreach parameter in request.Parameters Add a row and populate the fields
            foreach( ParameterDataElement pde in request.Parameters)
            {
                // Genereate parameter row
                ParameterFields.AddField();

                // Get count of the children
                int count = ParametersGrid.Children.Count;

                // Set the Key
                TextBox key = (TextBox)ParametersGrid.Children.Cast<UIElement>().ElementAt(count - 3);
                key.Text = pde.Key;

                // Set the Value
                TextBox value = (TextBox)ParametersGrid.Children.Cast<UIElement>().ElementAt(count - 2);
                value.Text = pde.Value;
            }

            // Set the ParametersGrid of Main window to the ParametersGrid variable
            ((MainWindow)System.Windows.Application.Current.MainWindow).ParametersGrid = ParametersGrid;
        }
    }
}
