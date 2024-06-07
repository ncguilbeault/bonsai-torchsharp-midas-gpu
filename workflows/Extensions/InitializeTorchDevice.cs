using System;
using System.ComponentModel;
using System.Reactive.Linq;
using Bonsai;
using TorchSharp;
using static TorchSharp.torch;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [DefaultProperty("DeviceType")]
    public class InitializeTorchDevice
    {
        public DeviceType DeviceType { get; set; }

        public IObservable<Device> Process()
        {
            TorchSharpInitializer.Initialize();
            return Observable.Defer(() =>
            {
                InitializeDeviceType(DeviceType);
                return Observable.Return(new Device(DeviceType));
            });
        }
    }
}
