﻿

#pragma checksum "C:\Users\codecamp\documents\visual studio 2012\Projects\GamePS\GamePS\Gameplay.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "17ADD67ACCCA3E0CF91181E32166917F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamePS
{
    partial class Gameplay : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 13 "..\..\Gameplay.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.uTouched;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 17 "..\..\Gameplay.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.DeleteAll;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 18 "..\..\Gameplay.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.spw;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 19 "..\..\Gameplay.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.go_back;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


