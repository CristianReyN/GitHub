using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExecuteCompression
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(true);
      //Application.Run(new Form1());

      AutoFeedCompress.ClassCompress clsCompress = new AutoFeedCompress.ClassCompress();

      clsCompress.StartProcess(true, 0);
    }
  }
}
