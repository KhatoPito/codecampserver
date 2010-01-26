using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using MvcContrib.UI.InputBuilder.Helpers;

namespace UITestHelper
{
    public class InputWrapperFactoryOverride : InputWrapperFactory
    {
        public InputWrapperFactoryOverride()
        {            
            Insert(0,new ScriptWrittenInputWrapperFactory());
            Insert(0, new DateTimeInputWrapperFactory());
        }
    }
}