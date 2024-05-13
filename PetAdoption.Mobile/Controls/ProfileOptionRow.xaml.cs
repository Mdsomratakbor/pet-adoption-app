namespace PetAdoption.Mobile.Controls;

public partial class ProfileOptionRow : ContentView
{
  
    public ProfileOptionRow()
    {
        InitializeComponent();
    }
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ProfileOptionRow), string.Empty);
    public string Text 
    { 
        get => GetValue(TextProperty) as string; 
        set => SetValue(TextProperty, value); 
    }
    public event EventHandler<string> Tapped;

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Tapped.Invoke(this, Text);
    }
}