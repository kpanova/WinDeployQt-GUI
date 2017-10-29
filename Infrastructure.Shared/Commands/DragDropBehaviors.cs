using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Markup;

namespace Infrastructure.Shared.Commands
{

        public static class DragDropBehaviors
        {
            public static readonly DependencyProperty DragProperty = DependencyProperty.RegisterAttached("Drag", typeof(object), typeof(DragDropBehaviors),
                                                  new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDragChanged)));

            public static readonly DependencyProperty DropProperty = DependencyProperty.RegisterAttached("Drop", typeof(object), typeof(DragDropBehaviors),
                                                      new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDropChanged)));

            public static void SetDrag(DependencyObject obj, object value)
            {
                obj.SetValue(DragProperty, value);
            }

            public static object GetDrag(DependencyObject obj)
            {
                return obj.GetValue(DragProperty);
            }

            public static void SetDrop(DependencyObject obj, object value)
            {
                obj.SetValue(DropProperty, value);
            }

            public static object GetDrop(DependencyObject obj)
            {
                return obj.GetValue(DropProperty);
            }


            private static void OnDragChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                //MessageBox.Show("I am in DragDropBehaviors");
                TreeView parent = (TreeView)obj;
                object data = FindTreeLevel(obj);
                if (data != null)
                {
                    DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
                }
            }

            private static object GetDataFromTreeView(TreeView source, Point point, DependencyObject control)
            {
                UIElement uiElement = source.InputHitTest(point) as UIElement;
                if (control != null)
                {
                    object data = DependencyProperty.UnsetValue;
                    while (data == DependencyProperty.UnsetValue)
                    {
                        data = source.ItemContainerGenerator.ItemFromContainer(uiElement);

                        if (data == DependencyProperty.UnsetValue)
                        {
                            uiElement = VisualTreeHelper.GetParent(uiElement) as UIElement;

                        }

                        if (uiElement == source)
                        {
                            return null;
                        }
                    }

                    if (data != DependencyProperty.UnsetValue)
                    {
                        return data;
                    }
                }

                return null;
            }

            private static object FindTreeLevel(DependencyObject control)
            {
                int i = 0;
                var parent = VisualTreeHelper.GetParent(control);
                while (!(parent is TreeView) && (parent != null))
                {
                    if (parent is TreeViewItem)
                        i++;
                    parent = VisualTreeHelper.GetParent(parent);
                }
                return parent;
            }

            private static void OnDropChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                var parent = VisualTreeHelper.GetParent(obj);
                if ((parent is TreeView) && (parent != null))
                {
                    TreeView source = (TreeView)parent;
                    source.Drop += (sender, args) =>
                    {
                        var data = args.Data.GetData(typeof(object));
                        source.Items.Remove(data);
                        source.Items.Add(data);
                    };
                }
            }
        
    
}
}
