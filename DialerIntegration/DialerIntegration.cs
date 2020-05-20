using System;
using System.Xml.Linq;

namespace DialerIntegration {
    [MyPhonePlugins.CRMPluginLoader]
    public class DialerIntegration {
        private static DialerIntegration instance = null;               // Holds the instance
        private MyPhonePlugins.IMyPhoneCallHandler callHandler = null;  // Holds the handler

        [MyPhonePlugins.CRMPluginInitializer]
        public static void Loader(MyPhonePlugins.IMyPhoneCallHandler callHandler) {
            instance = new DialerIntegration(callHandler);
        }

        // Constructor for plugin, to add event handler
        private DialerIntegration(MyPhonePlugins.IMyPhoneCallHandler callHandler) {
            this.callHandler = callHandler;

            callHandler.OnCallStatusChanged += new MyPhonePlugins.CallInfoHandler(callHandler_OnCallStatusChanged);
        }

        // Processes the status of the call
        private void callHandler_OnCallStatusChanged(object sender, MyPhonePlugins.CallStatus callInfo) {
            var extensionInfo = sender as MyPhonePlugins.IExtensionInfo;

            switch (callInfo.State) {
                case MyPhonePlugins.CallState.Connected: {
                        XElement configXml = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + @"\config.xml");
                        string url = configXml.Element("Url").Value.ToString();

                        if (!string.IsNullOrEmpty(callInfo.OtherPartyNumber)) {
                            url = url + callInfo.OtherPartyNumber;
                            System.Diagnostics.Process.Start(url);
                        }

                        break;
                    };
                case MyPhonePlugins.CallState.Ringing: {
                        break;
                    };
                case MyPhonePlugins.CallState.Undefined: {
                        break;
                    };
                default: {
                        break;
                    };
            }
        }
    }
}
