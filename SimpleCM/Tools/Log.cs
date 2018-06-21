using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SimpleCM.Tools
{
    class Log
    {
        public interface ILog
        {
            void WriteLine(string message);
        }


        public const int LEVEL_INFO = 3;
        public const int LEVEL_DEBUG = 2;
        public const int LEVEL_WARNING = 1;
        public const int LEVEL_ERROR = 0;

        private const string LOG_FILE_NAME = "app.log";
        private const int FILE_SIZE_LIMIT = 1024 * 1024 * 3;

        private static int mLogLevel = LEVEL_INFO;

        private static ILog instance = new LogImp();

        public static void SetLevel(int level)
        {
            mLogLevel = level;
        }

        public static void I(string tag, string message)
        {
            StackFrame stack = new StackFrame(1, true);
            string msg = string.Format("[file: {0}] | [func: {1}] | [tid:{2}] | {3}",
                     TrimFileName(stack.GetFileName()),
                     stack.GetMethod().Name,
                     Thread.CurrentThread.ManagedThreadId,
                     message);
            if (mLogLevel >= LEVEL_INFO)
            {
                instance.WriteLine(msg);
            }
        }

        public static void D(string tag, string message)
        {
            StackFrame stack = new StackFrame(1, true);
            string msg = string.Format("[file: {0}] | [func: {1}] | [tid:{2}] | {3}",
                     TrimFileName(stack.GetFileName()),
                     stack.GetMethod().Name,
                     Thread.CurrentThread.ManagedThreadId,
                     message);
            if (mLogLevel >= LEVEL_DEBUG)
            {
                instance.WriteLine(msg);
            }

        }

        public static void W(string tag, string message)
        {
            StackFrame stack = new StackFrame(1, true);
            string msg = string.Format("[file: {0}] | [func: {1}] | [line: {2}] | [col: {3}] | [tid:{4}] | {5}",
                     TrimFileName(stack.GetFileName()),
                     stack.GetMethod().Name,
                     stack.GetFileLineNumber(),
                     stack.GetFileColumnNumber(),
                     Thread.CurrentThread.ManagedThreadId,
                     message);
            if (mLogLevel >= LEVEL_WARNING)
            {
                instance.WriteLine(msg);
            }

        }

        public static void E(string tag, string message)
        {
            StackFrame stack = new StackFrame(1, true);
            string msg = string.Format("[file: {0}] | [func: {1}] | [line: {2}] | [col: {3}] | [tid:{4}] | {5}",
                     TrimFileName(stack.GetFileName()),
                     stack.GetMethod().Name,
                     stack.GetFileLineNumber(),
                     stack.GetFileColumnNumber(),
                     Thread.CurrentThread.ManagedThreadId,
                     message);
            if (mLogLevel >= LEVEL_ERROR)
            {
                instance.WriteLine(msg);
            }
        }

        private static string TrimFileName(string filename)
        {
            try
            {
                string newname = filename.Replace("\\", "/");
                int istartpos = newname.TrimEnd('/').LastIndexOf('/');
                return newname.Substring(istartpos + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return filename;

        }

        class LogImp : ILog
        {
            TextWriterTraceListener logHolder;

            public LogImp()
            {
                Init();
            }

            public void WriteLine(string message)
            {

                string format = "yyyyMMdd HH:mm:ss.fff";
                string time = DateTime.Now.ToString(format);

                string outputMsg = string.Format("[{0}] : {1}", time, message);


                WriteToFile(outputMsg);

                lock (this)
                {
                    Trace.WriteLine(outputMsg);
                }
            }

            private void WriteToFile(string line)
            {
                try
                {
                    lock (this)
                    {
                        if (logHolder == null)
                        {
                            Init();
                        }
                        if (logHolder != null)
                        {
                            logHolder.WriteLine(line);
                            logHolder.Flush();
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Logger:WriteToFile Error: " + e.Message);
                }
            }

            public void Init()
            {
                try
                {
                    string path = PathUtils.GetLogPath();

                    path += "\\" + LOG_FILE_NAME;
                    Debug.WriteLine("DVPFS Log Path: " + path);

                    if (File.Exists(path))
                    {
                        FileInfo log = new FileInfo(path);

                        if (log.Length >= FILE_SIZE_LIMIT)
                        {
                            log.Delete();
                        }
                    }

                    logHolder = new TextWriterTraceListener(path, "LogHelper");

                    I("Log:Init", "----Log Start");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Logger:Init Error: " + e.Message);
                    logHolder = null;
                }
            }

        }
    }
}
