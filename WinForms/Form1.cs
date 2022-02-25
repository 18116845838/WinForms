using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
	public partial class Form1 : Form
	{


		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//程序加载的时候取消跨线程的 检查
			Control.CheckForIllegalCrossThreadCalls = false;
		}
		
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			FromService fromService = new FromService();
			fromService.Show();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				textBox3.Text = textBox1.Text + ":" + textBox2.Text;
				IPAddress ip = IPAddress.Parse(textBox1.Text);
				IPEndPoint ips = new IPEndPoint(ip, int.Parse(textBox2.Text));
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Bind(ips);
				socket.Listen(1);
				textBox3.AppendText("\n\t正在等待客户端连接");


				Thread thread = new Thread(Listen);
				thread.IsBackground = true;
				thread.Start(socket);
			}
			catch (Exception)
			{

			}


		}
		Socket socket;
		void Listen(object o)
		{
			try
			{
				while (true)
				{
					send = socket.Accept();
					textBox3.AppendText(send.RemoteEndPoint.ToString());
					Thread thread = new Thread(GetMessage);
					thread.IsBackground = true;
					thread.Start(send);
				}

			}
			catch (Exception)
			{

				throw;
			}
			
		}

		void GetMessage(object o)
		{
			Socket socket = o as Socket;
			try
			{
				while (true)
				{
					byte[] buffer = new byte[1024 * 1024 * 2];
					int r = socket.Receive(buffer);
					if (r == 0)
					{

						break;
					}
					string str = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
					textBox3.AppendText("\n" + str);
				}
			}
			catch (Exception)
			{

				throw;
			}
			
		}
		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}
		Socket send;
		private void button1_Click_1(object sender, EventArgs e)
		{
			try
			{
				string msg = textBox4.Text;
				byte[] vs = Encoding.UTF8.GetBytes(msg);

				send.Send(vs, vs.Length, 0);
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		private void textBox4_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
