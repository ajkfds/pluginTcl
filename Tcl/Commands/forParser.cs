using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginTcl.Tcl.Commands
{
    class forParser
    {
        // for start test next body
        /*
         
        For is a looping command, similar in structure to the C for statement.
        The start, next, and body arguments must be Tcl command strings, and test is an expression string.
        The for command first invokes the Tcl interpreter to execute start.
        Then it repeatedly evaluates test as an expression; 

        if the result is non-zero it invokes the Tcl interpreter on body, 
        then invokes the Tcl interpreter on next, 
        then repeats the loop. The command terminates when test evaluates to 0.

        If a continue command is invoked within body then any remaining commands in the current execution of body are skipped; 
        processing continues by invoking the Tcl interpreter on next, then evaluating test, and so on. If a break command is invoked within body or next, then the for command will return immediately. The operation of break and continue are similar to the corresponding statements in C. 
        For returns an empty string. 
        
        Note: test should almost always be enclosed in braces. 
        If not, variable substitutions will be made before the for command starts executing, 
        which means that variable changes made by the loop body will not be considered in the expression. 
        This is likely to result in an infinite loop. 
        If test is enclosed in braces, variable substitutions are delayed until the expression is evaluated (before each loop iteration), 
        so changes in the variables will be visible. See below for an example: 
         */
    }
}
