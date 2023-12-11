using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;

namespace ThreeApps
{
    public partial class Lab6 : Form
    {
        private NamedPipeServerStream serverStream;
        private static EventWaitHandle eventHandle;

        private int amount;
        private double minValue;
        private double maxValue;
        private string secondProgramPath = "\"C:\\Users\\Горовий Олександр\\source\\repos\\ThreeApps\\Object2\\bin\\Debug\\net6.0-windows\\Object2.exe\"";
        private string thirdProgramPath = "\"C:\\Users\\Горовий Олександр\\source\\repos\\ThreeApps\\Object3\\bin\\Debug\\net6.0-windows\\Object3.exe\"";
        private Process object2Process;
        private Process object3Process;

        public Lab6()
        {
            InitializeComponent();
            StartPipeServer();
            FormClosing += Lab6_FormClosing;
        }

        private void Lab6_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > numericUpDown3.Value)
            {
                numericUpDown2.Value = numericUpDown3.Value - 1;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value < numericUpDown2.Value)
            {
                numericUpDown3.Value = numericUpDown2.Value + 1;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //stopProcesses();

            if (object2Process == null || object2Process.HasExited)
            {
                StartApp2();
            }

            amount = (int)numericUpDown1.Value;
            minValue = (double)numericUpDown2.Value;
            maxValue = (double)numericUpDown3.Value;

            try
            {

                Thread.Sleep(1000); // 1 second delay

                byte[] intBytes = BitConverter.GetBytes(amount);
                byte[] doubleBytes1 = BitConverter.GetBytes(minValue);
                byte[] doubleBytes2 = BitConverter.GetBytes(maxValue);

                byte[] buffer = intBytes.Concat(doubleBytes1).Concat(doubleBytes2).ToArray();

                serverStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending data: " + ex.Message);
            }



            if (object3Process == null || object3Process.HasExited)
            {
                StartApp3();
            }

            StartListeningForObject2();
        }

        private static void StartListeningForObject2()
        {
            try
            {
                eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, "EventObject2To1");

                Thread listenThread = new Thread(ListenForObject2Event);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting Object1 listening thread: " + ex.Message);
            }
        }

        private static void ListenForObject2Event()
        {
            try
            {
                while (true)
                {
                    eventHandle.WaitOne();

                    eventHandle.Reset();

                    Console.WriteLine("Object1 received the signal from Object2");

                    SignalObject3();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Object1 listening thread: " + ex.Message);
            }
        }

        private static void SignalObject3()
        {
            try
            {
                using (EventWaitHandle eventHandle = EventWaitHandle.OpenExisting("EventObject1Completed"))
                {
                    eventHandle.Set();
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error signaling Object3: " + ex.Message);
            }
        }

        private void Lab6_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopProcesses();
            ClosePipeServer();
        }

        private void ClosePipeServer()
        {
            try
            {
                if (serverStream != null)
                {
                    serverStream.Close();
                    serverStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing pipe server: " + ex.Message);
            }
        }

        private void stopProcesses()
        {
            if (object3Process != null && !object3Process.HasExited)
            {

                //object3Process.CloseMainWindow();

                //if (!object3Process.WaitForExit(5000))
                //{
                    object3Process.Kill();
                //}
            }

            if (object2Process != null && !object2Process.HasExited)
            {

                //object2Process.CloseMainWindow();

                //if (!object2Process.WaitForExit(5000))
                //{
                    object2Process.Kill();
                //}
            }
        }



        private void StartPipeServer()
        {
            serverStream = new NamedPipeServerStream("Lab6_Pipe", PipeDirection.Out);
            Thread pipeServerThread = new Thread(WaitForConnection);
            pipeServerThread.Start();
        }

        private void WaitForConnection()
        {
            // Wait for a connection from App2
            serverStream.WaitForConnection();
        }

        private void StartApp2()
        {
            try
            {
                // Replace "App2.exe" with the actual executable name of App2
                ProcessStartInfo object2Info = new ProcessStartInfo(secondProgramPath);
                object2Process = Process.Start(object2Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting App2: " + ex.Message);
            }
        }

        private void StartApp3()
        {
            try
            {
                // Replace "App2.exe" with the actual executable name of App2
                ProcessStartInfo object3Info = new ProcessStartInfo(thirdProgramPath);
                object3Process = Process.Start(object3Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting App2: " + ex.Message);
            }
        }


    }
}