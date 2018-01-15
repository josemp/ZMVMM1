using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zmvvm1
{
    public delegate void executeCommandDel(string parameter);
    public delegate bool canExecuteCommandDel(string parameter);
   
    public class CommandClass 
    {
        executeCommandDel executeCommand = null;
        canExecuteCommandDel canExecuteCommand = null;
   


            private readonly DelegateCommandClass<string> _command;

            public DelegateCommandClass<string> command
            {
                get { return _command; }
            }
        public void notifyChange()
            {            
                _command.RaiseCanExecuteChanged();
            }
        public CommandClass(canExecuteCommandDel canExecuteCommand, executeCommandDel executeCommand)
        {
            this.executeCommand = executeCommand;
            this.canExecuteCommand = canExecuteCommand;
            _command = new DelegateCommandClass<string>(
             (s) => { if (executeCommand!=null) executeCommand(s); }, //Execute
             (s) =>
             {
                 if (canExecuteCommand != null) return canExecuteCommand(s);
                 else return (true);
             } //CanExecute
             );


        }
    }
}
