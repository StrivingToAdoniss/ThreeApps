using System;
using System.Windows.Forms;

namespace Object3
{
    public partial class Object3 : Form
    {

        private EventWaitHandle eventHandle;
        private Thread listenThread;
        public Object3()
        {
            InitializeComponent();
            InitializeEventHandle();
            StartListeningForObject1Signal();

            FormClosing += object3_FormClosing;
        }

        private void InitializeEventHandle()
        {
            try
            {
                // Assuming "EventObject1Completed" is the event name shared between Object1 and Object3
                eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, "EventObject1Completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing event handle: " + ex.Message);
            }
        }

        private void StartListeningForObject1Signal()
        {
            try
            {
                // Start a thread to continuously listen for the signal from Object1
                listenThread = new Thread(ListenForObject1Signal);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting Object3 listening thread: " + ex.Message);
            }
        }

        private void ListenForObject1Signal()
        {
            try
            {
                while (true)
                {
                    // Wait for the event to be signaled by Object1
                    eventHandle.WaitOne();

                    // Reset the event to non-signaled state
                    eventHandle.Reset();

                    // Perform tasks in response to the signal from Object1
                    // LoadClipboardData method will be called
                    Invoke(new Action(LoadClipboardData));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Object3 listening thread: " + ex.Message);
            }
        }

        private void LoadClipboardData()
        {

            if (Clipboard.ContainsText())
            {

                string clipboardText = Clipboard.GetText();

                double[] dataArray = ParseTextToArray(clipboardText);

                Array.Sort(dataArray);

                DisplaySortedArray(dataArray);
            }
            else
            {
                listBox1.Items.Add("Clipboard does not contain text data.");
            }
        }

        private double[] ParseTextToArray(string text)
        {

            string[] stringValues = text.Split(';');

            double[] doubleArray = new double[stringValues.Length];
            for (int i = 0; i < stringValues.Length; i++)
            {
                if (double.TryParse(stringValues[i], out double parsedValue))
                {
                    doubleArray[i] = parsedValue;
                }
                else
                {
                    listBox1.Items.Add($"Error parsing value at index {i}: {stringValues[i]}");
                }
            }

            return doubleArray;
        }

        private void DisplaySortedArray(double[] array)
        {

            listBox1.Items.Clear();

            foreach (double value in array)
            {
                listBox1.Items.Add(value);
            }
        }

        private void object3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Prevent the form from closing
                e.Cancel = true;
            }
            else 
            {
                listenThread.Abort();
            }



        }
    }
}