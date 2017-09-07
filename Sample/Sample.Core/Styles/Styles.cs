using Xamarin.Forms;

namespace Sample.Core.Styles
{
    public static class Styles
    {
        public static readonly Style LeftRightIndentContainer = new Style(typeof(Layout))
        {
            Setters =
            {
                new Setter
                {
                    Property = Layout.PaddingProperty,
                    Value = new Thickness(Dimensions.Indent, 0)
                }
            }
        };
    }
}