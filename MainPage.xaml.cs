namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        IServiceTest Services;
        private readonly IMessage message;

        public string MessageToast { get; set; }

        public int CounterSnack { get; set; }

        public MainPage(IServiceTest Services_, IMessage message)
        {
            InitializeComponent();
            Services = Services_;
            this.message = message;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            Services.Start();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Services.Stop();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }

}
