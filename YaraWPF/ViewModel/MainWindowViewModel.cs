using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using YaraSharp;

namespace YaraWPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private YSInstance ysInstance = new YSInstance();

        public MainWindowViewModel()
        {
            InitializeYara();
        }

        private void InitializeYara()
        {
            var externals = new Dictionary<string, object>()
            {
                { "filename", string.Empty },
                { "filepath", string.Empty },
                { "extension", string.Empty }
            };

            var ruleFilenames = System.IO.Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "rules"), "*.yara").ToList();

            using (YSContext context = new YSContext())
            {
                using (YSCompiler compiler = ysInstance.CompileFromFiles(ruleFilenames, externals))
                {
                    YSRules rules = compiler.GetRules();
                    
                    YSReport errors = compiler.GetErrors();
                    
                    YSReport warnings = compiler.GetWarnings();

                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}