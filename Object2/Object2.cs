using System.IO.Pipes;

namespace Object2
{
    public partial class Object2 : Form
    {

        private NamedPipeClientStream clientStream;
        public Object2(int n, double minValue, double maxValue)
        {
            InitializeComponent();
            StartPipeClient();

            Thread receiveThread = new Thread(ReceiveData);
            receiveThread.Start();

            

            FormClosing += object2_FormClosing;

        }

        private void StartPipeClient()
        {
            clientStream = new NamedPipeClientStream(".", "Lab6_Pipe", PipeDirection.In);
            clientStream.Connect();
        }



        private void ReceiveData()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = clientStream.Read(buffer, 0, buffer.Length);

                    int receivedIntValue = BitConverter.ToInt32(buffer, 0);
                    double receivedDoubleValue1 = BitConverter.ToDouble(buffer, sizeof(int));
                    double receivedDoubleValue2 = BitConverter.ToDouble(buffer, sizeof(int) + sizeof(double));


                    Invoke(new Action(() =>
                    {

                        LoadGeneratedList(receivedIntValue, receivedDoubleValue1, receivedDoubleValue2);
                        Thread.Sleep(1000);
                        SignalObject1();

                    }));


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving data: " + ex.Message);
            }
        }

        private void SignalObject1()
        {
            try
            {
                using (EventWaitHandle eventHandle = EventWaitHandle.OpenExisting("EventObject2To1"))
                {
                    eventHandle.Set();
                }
            }
            catch (WaitHandleCannotBeOpenedException ex)
            {
                MessageBox.Show("Error opening handling Object1: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error signaling Object1: " + ex.Message);
            }
        }

        private void LoadGeneratedList(int n, double minValue, double maxValue)
        {
            if (n == 0)
            {
                listBox1.Items.Add("n is set to 0");
            }
            else if (n >= 1 && n <= 100)
            {
                double[] vectorOfDoubles = GenerateRandomDoubles(n, minValue, maxValue);

                DisplayDoublesInListBox(vectorOfDoubles);
                CopyDoublesToClipboard(vectorOfDoubles);


            }
            else
            {
                listBox1.Items.Add("Invalid value for n. It should be between 1 and 100.");
            }
        }

        private void DisplayDoublesInListBox(double[] vectorOfDoubles)
        {
            listBox1.Items.Clear();

            foreach (double value in vectorOfDoubles)
            {
                listBox1.Items.Add(value.ToString());
            }
        }

        private double[] GenerateRandomDoubles(int n, double minValue, double maxValue)
        {
            Random random = new Random();
            double[] result = new double[n];

            for (int i = 0; i < n; i++)
            {
                double randomValue = random.NextDouble() * (maxValue - minValue) + minValue;
                result[i] = Math.Round(randomValue, 2);
            }

            return result;
        }



        private void CopyDoublesToClipboard(double[] doubleArray)
        {
            string doubleArrayString = string.Join(';', doubleArray);

            Clipboard.SetText(doubleArrayString);

            if (Clipboard.ContainsText())
            {
                Console.WriteLine("Array of doubles has been copied to the Clipboard.");
            }
            else
            {
                Console.WriteLine("Failed to copy array of doubles to the Clipboard.");
            }
        }

        private void object2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Prevent the form from closing
                e.Cancel = true;
            }
            else 
            {
                clientStream.Close();
            }


        }
    }

}