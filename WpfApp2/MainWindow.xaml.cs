using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;




namespace WpfApp2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readdata = null;
        byte[] outdata = null;
        int i = 0;
        int o = 0;
        static double max = 40;
        static double min = -max;
        static double roznica = 13;


        public MainWindow()
        {
            InitializeComponent();
            disconnectbtn.IsEnabled = false;
            send.IsEnabled = false;
            manual.IsEnabled = false;
            auto.IsEnabled = false;
            keyboard.IsEnabled = false;
            motorL.IsEnabled = false;
            motorR.IsEnabled = false;

        }

        private void Test()
        {
            while (i != 0)
            {
                if (clientSocket.Connected == false)
                {
                    EndConnection();
                }
            }

        }

        private void EndConnection()
        {
            if (!Dispatcher.CheckAccess())
            {
                this.Dispatcher.Invoke(new Action(EndConnection));
            }
            else
            {
                i = 0;
                o = 0;
                serverStream.Close();
                serverStream = null;
                clientSocket.Close();
                clientSocket = new TcpClient();
                off.Visibility = Visibility.Visible;
                on.Visibility = Visibility.Collapsed;
                disconnectbtn.IsEnabled = false;
                manual.IsEnabled = false;
                auto.IsEnabled = false;
                keyboard.IsEnabled = false;
                send.IsEnabled = false;
                connectbtn.IsEnabled = true;
                motorL.Value = 0;
                motorR.Value = 0;
                led1.IsChecked = false;
                led2.IsChecked = false;
                motorL.IsEnabled = false;
                motorR.IsEnabled = false;
                sensor1.Value = 0;
                sensor2.Value = 0;
                sensor3.Value = 0;
                sensor4.Value = 0;
                sensor5.Value = 0;
                st1.Text = "";
                st2.Text = "";
                st3.Text = "";
                st4.Text = "";
                st5.Text = "";
                battery.Text = "";
                income.Text = "";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            off.Visibility = Visibility.Collapsed;
            con.Visibility = Visibility.Visible;
            Thread coneThread = new Thread(cone);
            coneThread.Start();

        }
        private void cone()
        {
            Thread.Sleep(500);
            if (!Dispatcher.CheckAccess())
            {
                this.Dispatcher.Invoke(new Action(cone));
            }
            else
            {
                if (clientSocket.Connected == false)
                {

                    try
                    {
                        if (ipbtn.IsEnabled == false) clientSocket.Connect(ipadress.Text, Int32.Parse(port.Text));
                        
                        i = 1;
                        Thread ctThread = new Thread(GetMessage);
                        ctThread.Start();
                        Thread testThread = new Thread(Test);
                        testThread.Start();
  
                        con.Visibility = Visibility.Collapsed;
                        on.Visibility = Visibility.Visible;
                        connectbtn.IsEnabled = false;
                        disconnectbtn.IsEnabled = true;
                        send.IsEnabled = true;
                        manual.IsEnabled = true;
                        auto.IsEnabled = true;
                        keyboard.IsEnabled = true;
                    }

                    catch (System.Net.Sockets.SocketException blad)
                    {

                        MessageBox.Show(blad.Message);
                        con.Visibility = Visibility.Collapsed;
                        off.Visibility = Visibility.Visible;

                    }
                    catch (System.FormatException blad)
                    {

                        MessageBox.Show(blad.Message);
                        con.Visibility = Visibility.Collapsed;
                        off.Visibility = Visibility.Visible;

                    }

                }
            }

        }


        private void GetMessage()
        {

            if (clientSocket.Connected == true)
            {
                string returndata;
                serverStream = clientSocket.GetStream();



                while (serverStream != null)
                {

                    var buffsize = clientSocket.ReceiveBufferSize;
                    byte[] instream = new byte[buffsize];

                    try
                    {
                        serverStream.Read(instream, 0, buffsize);
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Rozłączono");
                    }


                    returndata = System.Text.Encoding.ASCII.GetString(instream);

                    readdata = returndata;
                    Msg();



                }
            }




        }

        private void Msg()
        {

            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    this.Dispatcher.Invoke(new Action(Msg));
                }
                else
                {
                    income.Text = readdata;
                    string bat = readdata.Remove(0, 3);
                    bat = bat.Remove(4);

                    string bat1 = bat.Remove(0, 2);
                    string bat2 = bat.Remove(2);
                    bat = bat1 + bat2;

                    try
                    {
                        int bati = Convert.ToInt32(bat, 16);
                        bat = Convert.ToString(bati);
                        battery.Text = bat + "mV";
                    }
                    catch (System.FormatException)
                    {

                    }

                    string sens1 = readdata.Remove(0, 7);
                    sens1 = sens1.Remove(4);

                    try
                    {
                        int s1 = Convert.ToInt32(sens1, 16);
                        if (s1 > 2000)
                        {
                            string s11 = sens1.Remove(0, 2);
                            string s12 = sens1.Remove(2);
                            sens1 = s11 + s12;
                            s1 = Convert.ToInt32(sens1, 16);
                            sens1 = Convert.ToString(s1);
                            st1.Text = sens1;
                        }
                        else
                        {
                            sens1 = Convert.ToString(s1);
                            st1.Text = sens1;
                        }

                        sensor1.Value = s1;
                    }
                    catch (System.FormatException)
                    {

                    }
                    string sens2 = readdata.Remove(0, 11);
                    sens2 = sens2.Remove(4);

                    try
                    {
                        int s2 = Convert.ToInt32(sens2, 16);
                        if (s2 > 2000)
                        {
                            string s21 = sens2.Remove(0, 2);
                            string s22 = sens2.Remove(2);
                            sens2 = s21 + s22;
                            s2 = Convert.ToInt32(sens2, 16);
                            sens2 = Convert.ToString(s2);
                            st2.Text = sens2;
                        }
                        else
                        {
                            sens2 = Convert.ToString(s2);
                            st2.Text = sens2;
                        }

                        sensor2.Value = s2;
                    }
                    catch (System.FormatException)
                    {

                    }
                    string sens3 = readdata.Remove(0, 15);
                    sens3 = sens3.Remove(4);

                    try
                    {
                        int s3 = Convert.ToInt32(sens3, 16);
                        if (s3 > 2000)
                        {
                            string s31 = sens3.Remove(0, 2);
                            string s32 = sens3.Remove(2);
                            sens3 = s31 + s32;
                            s3 = Convert.ToInt32(sens3, 16);
                            sens3 = Convert.ToString(s3);
                            st3.Text = sens3;
                        }
                        else
                        {
                            sens3 = Convert.ToString(s3);
                            st3.Text = sens3;
                        }

                        sensor3.Value = s3;
                    }
                    catch (System.FormatException)
                    {

                    }
                    string sens4 = readdata.Remove(0, 19);
                    sens4 = sens4.Remove(4);

                    try
                    {
                        int s4 = Convert.ToInt32(sens4, 16);
                        if (s4 > 2000)
                        {
                            string s41 = sens4.Remove(0, 2);
                            string s42 = sens4.Remove(2);
                            sens4 = s41 + s42;
                            s4 = Convert.ToInt32(sens4, 16);
                            sens4 = Convert.ToString(s4);
                            st4.Text = sens4;
                        }
                        else
                        {
                            sens4 = Convert.ToString(s4);
                            st4.Text = sens4;
                        }

                        sensor4.Value = s4;
                    }
                    catch (System.FormatException)
                    {

                    }
                    string sens5 = readdata.Remove(0, 23);
                    sens5 = sens5.Remove(4);

                    try
                    {
                        int s5 = Convert.ToInt32(sens5, 16);
                        if (s5 > 2000)
                        {
                            string s51 = sens5.Remove(0, 2);
                            string s52 = sens5.Remove(2);
                            sens5 = s51 + s52;
                            s5 = Convert.ToInt32(sens5, 16);
                            sens5 = Convert.ToString(s5);
                            st5.Text = sens5;
                        }
                        else
                        {
                            sens5 = Convert.ToString(s5);
                            st5.Text = sens5;
                        }
                        sensor5.Value = s5;
                    }
                    catch (System.FormatException)
                    {

                    }


                }
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                
            }



        }
        private void AutoMessage()
        {

            if (clientSocket.Connected == true)
            {

                while (o == 2)
                {
                    try
                    {
                        AutoControl();
                        try
                        {
                            serverStream.Write(outdata, 0, outdata.Length);
                        }
                        catch (System.IO.IOException)
                        {

                        }
                        serverStream.Flush();
                        Thread.Sleep(250);
                    }
                    catch (System.NullReferenceException)
                    {

                    }
                }
            }


        }

        private void AutoControl()
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    this.Dispatcher.Invoke(new Action(AutoControl));
                }
                else
                {
                    string start = ("[");
                    string stop = ("]");
                    int lwi = System.Convert.ToInt16(motorL.Value);
                    string lws = System.Convert.ToString(lwi);
                    string lw = lwi.ToString("X");
                    if (lw.Length > 2) lw = lw.Remove(0, 6);
                    if (lwi < 16 && lwi > -1)
                    {
                        lw = "0" + lw;
                    }
                    int rwi = System.Convert.ToInt16(motorR.Value);
                    string rws = System.Convert.ToString(rwi);
                    string rw = rwi.ToString("X");
                    if (rw.Length > 2) rw = rw.Remove(0, 6);
                    if (rwi < 16 && rwi > -1)
                    {
                        rw = "0" + rw;
                    }
                    int led_1i = System.Convert.ToInt16(led1.IsChecked);
                    int led_2i = 0;
                    if (led2.IsChecked == true)
                    {
                        led_2i = System.Convert.ToInt16(led2.IsChecked) + 1;
                    }
                    int led_12i = led_1i + led_2i;
                    string led_12 = System.Convert.ToString(led_12i);

                    valueL.Text = lws;
                    valueR.Text = rws;

                    string outstring = start + "0" + led_12 + lw + rw + stop;

                    byte[] outstream = Encoding.ASCII.GetBytes(outstring);

                    sendmessage.Text = outstring;
                    outdata = outstream;
                }
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {

            }

        }

        private void KeyboardMessage()
        {

            if (clientSocket.Connected == true)
            {
                while (o == 1)
                {
                    try
                    {
                        KeyboardControl();
                        try
                        {
                            serverStream.Write(outdata, 0, outdata.Length);
                        }
                        catch (System.IO.IOException)
                        {

                        }
                        serverStream.Flush();
                        Thread.Sleep(50);
                    }
                    catch (System.NullReferenceException)
                    {

                    }
                }
            }

        }

        private void KeyboardControl()
        {
            try
            {
                if (!Dispatcher.CheckAccess())
                {
                    this.Dispatcher.Invoke(new Action(KeyboardControl));
                }
                else
                {

                    if (Keyboard.IsKeyDown(Key.Up))
                    {
                        if (motorL.Value < max) motorL.Value = motorL.Value + 3;
                        if (motorR.Value < max) motorR.Value = motorR.Value + 3;
                    }
                    else
                    {
                        if (motorL.Value > 0) motorL.Value = motorL.Value - 2;
                        if (motorR.Value > 0) motorR.Value = motorR.Value - 2;
                    }

                    if (Keyboard.IsKeyDown(Key.Down))
                    {
                        if (motorL.Value > min) motorL.Value = motorL.Value - 3;
                        if (motorR.Value > min) motorR.Value = motorR.Value - 3;
                    }
                    else
                    {
                        if (motorL.Value < 0) motorL.Value = motorL.Value + 2;
                        if (motorR.Value < 0) motorR.Value = motorR.Value + 2;
                    }

                    if (Keyboard.IsKeyDown(Key.Left))
                    {
                        if (motorL.Value > 0 && motorL.Value < max + roznica && Keyboard.IsKeyDown(Key.Up) && motorR.Value - motorL.Value < 2 * roznica) motorL.Value = motorL.Value - 3;
                        if (motorR.Value > 0 && motorR.Value < max + roznica && Keyboard.IsKeyDown(Key.Up) && motorR.Value - motorL.Value < 2 * roznica) motorR.Value = motorR.Value + 2;
                        if (motorL.Value < 0 && motorL.Value > min - roznica && Keyboard.IsKeyDown(Key.Down) && motorR.Value - motorL.Value > 2 * -roznica) motorL.Value = motorL.Value + 3;
                        if (motorR.Value < 0 && motorR.Value > min - roznica && Keyboard.IsKeyDown(Key.Down) && motorR.Value - motorL.Value > 2 * -roznica) motorR.Value = motorR.Value - 2;
                    }
                    else
                    {
                        if (Keyboard.IsKeyUp(Key.Right) && motorL.Value > max) motorL.Value = motorL.Value - 2;
                        if (Keyboard.IsKeyUp(Key.Right) && motorR.Value > max) motorR.Value = motorR.Value - 2;
                        if (Keyboard.IsKeyUp(Key.Right) && motorL.Value < min) motorL.Value = motorL.Value + 2;
                        if (Keyboard.IsKeyUp(Key.Right) && motorR.Value < min) motorR.Value = motorR.Value + 2;
                    }

                    if (Keyboard.IsKeyDown(Key.Right))
                    {
                        if (motorL.Value > 0 && motorL.Value < max + roznica && Keyboard.IsKeyDown(Key.Up) && motorL.Value - motorR.Value < 2 * roznica) motorL.Value = motorL.Value + 2;
                        if (motorR.Value > 0 && motorR.Value < max + roznica && Keyboard.IsKeyDown(Key.Up) && motorL.Value - motorR.Value < 2 * roznica) motorR.Value = motorR.Value - 3;
                        if (motorL.Value < 0 && motorL.Value > min - roznica && Keyboard.IsKeyDown(Key.Down) && motorL.Value - motorR.Value > 2 * -roznica) motorL.Value = motorL.Value - 2;
                        if (motorR.Value < 0 && motorR.Value > min - roznica && Keyboard.IsKeyDown(Key.Down) && motorL.Value - motorR.Value > 2 * -roznica) motorR.Value = motorR.Value + 3;
                    }
                    else
                    {
                        if (Keyboard.IsKeyUp(Key.Left) && motorL.Value > max) motorL.Value = motorL.Value - 2;
                        if (Keyboard.IsKeyUp(Key.Left) && motorR.Value > max) motorR.Value = motorR.Value - 2;
                        if (Keyboard.IsKeyUp(Key.Left) && motorL.Value < min) motorL.Value = motorL.Value + 2;
                        if (Keyboard.IsKeyUp(Key.Left) && motorR.Value < min) motorR.Value = motorR.Value + 2;
                    }

                    if (Keyboard.IsKeyDown(Key.L))
                    {
                        led1.IsChecked = !led1.IsChecked;
                    }

                    if (Keyboard.IsKeyDown(Key.P))
                    {
                        led2.IsChecked = !led2.IsChecked;
                    }


                    string start = ("[");
                    string stop = ("]");
                    int lwi = System.Convert.ToInt16(motorL.Value);
                    string lws = System.Convert.ToString(lwi);
                    string lw = lwi.ToString("X");
                    if (lw.Length > 2) lw = lw.Remove(0, 6);
                    if (lwi < 16 && lwi > -1)
                    {
                        lw = "0" + lw;
                    }
                    int rwi = System.Convert.ToInt16(motorR.Value);
                    string rws = System.Convert.ToString(rwi);
                    string rw = rwi.ToString("X");
                    if (rw.Length > 2) rw = rw.Remove(0, 6);
                    if (rwi < 16 && rwi > -1)
                    {
                        rw = "0" + rw;
                    }
                    int led_1i = System.Convert.ToInt16(led1.IsChecked);
                    int led_2i = 0;
                    if (led2.IsChecked == true)
                    {
                        led_2i = System.Convert.ToInt16(led2.IsChecked) + 1;
                    }
                    int led_12i = led_1i + led_2i;
                    string led_12 = System.Convert.ToString(led_12i);

                    valueL.Text = lws;
                    valueR.Text = rws;

                    string outstring = start + "0" + led_12 + lw + rw + stop;

                    byte[] outstream = Encoding.ASCII.GetBytes(outstring);

                    sendmessage.Text = outstring;
                    outdata = outstream;

                }
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {

            }


        }


        private void Send_Click(object sender, RoutedEventArgs e)
        {

            if (clientSocket.Connected == true)
            {
                if (manual.IsEnabled == false)
                {
                    byte[] outstream = Encoding.ASCII.GetBytes(outcome.Text);
                    sendmessage.Text = outcome.Text;
                    serverStream.Write(outstream, 0, outstream.Length);
                    serverStream.Flush();
                }
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (clientSocket.Connected == true)
            {

                i = 0;
                o = 0;
                serverStream.Close();
                serverStream = null;
                clientSocket.Close();
                clientSocket = clientSocket = new TcpClient();
                off.Visibility = Visibility.Visible;
                on.Visibility = Visibility.Collapsed;
                disconnectbtn.IsEnabled = false;
                manual.IsEnabled = false;
                auto.IsEnabled = false;
                keyboard.IsEnabled = false;
                send.IsEnabled = false;
                connectbtn.IsEnabled = true;
                motorL.Value = 0;
                motorR.Value = 0;
                led1.IsChecked = false;
                led2.IsChecked = false;
                sensor1.Value = 0;
                sensor2.Value = 0;
                sensor3.Value = 0;
                sensor4.Value = 0;
                sensor5.Value = 0;
                st1.Text = "";
                st2.Text = "";
                st3.Text = "";
                st4.Text = "";
                st5.Text = "";
                battery.Text = "";
                income.Text = "";
                valueL.Text = "";
                valueR.Text = "";
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Ipadress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            motorL.IsEnabled = false;
            motorR.IsEnabled = false;
            manual.IsEnabled = false;
            keyboard.IsEnabled = true;
            auto.IsEnabled = true;
            o = 0;
            motorL.Value = 0;
            motorR.Value = 0;
            led1.IsChecked = false;
            led2.IsChecked = false;


        }


        private void Auto_Click(object sender, RoutedEventArgs e)
        {
            motorL.IsEnabled = true;
            motorR.IsEnabled = true;
            auto.IsEnabled = false;
            keyboard.IsEnabled = true;
            manual.IsEnabled = true;
            o = 2;
            Thread autoThread = new Thread(AutoMessage);
            autoThread.Start();
            motorL.Value = 0;
            motorR.Value = 0;

        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)
        {
            motorL.IsEnabled = false;
            motorR.IsEnabled = false;
            keyboard.IsEnabled = false;
            auto.IsEnabled = true;
            manual.IsEnabled = true;
            o = 1;
            Thread keyboardThread = new Thread(KeyboardMessage);
            keyboardThread.Start();


        }

        private void Endbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            motorL.Value = 0;
            motorR.Value = 0;
            led1.IsChecked = false;
            led2.IsChecked = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Ipbtn_Click(object sender, RoutedEventArgs e)
        {
            
            ipbtn.IsEnabled = false;
            manually.Visibility = Visibility.Visible;
            

        }

        private void Numberbtn_Click(object sender, RoutedEventArgs e)
        {
            
            ipbtn.IsEnabled = true;
            manually.Visibility = Visibility.Collapsed;
            
        }

        private void sensor1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void motorR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        

    }
}
