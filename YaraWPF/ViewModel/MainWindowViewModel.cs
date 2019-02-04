using System.ComponentModel;
using System.Runtime.CompilerServices;
using YaraSharp;

namespace YaraWPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private YSInstance ysInstance = new YSInstance();

        public MainWindowViewModel()
        {

        }

        private void InitializeYara()
        {
            using (YSContext context = new YSContext())
            {
                //	Compiling rules
                using (YSCompiler compiler = ysInstance.CompileFromFiles(ruleFilenames, externals))
                {
                    //  Get compiled rules
                    YSRules rules = compiler.GetRules();

                    //  Get errors
                    YSReport errors = compiler.GetErrors();
                    //  Get warnings
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