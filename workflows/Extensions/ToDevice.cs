using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;
using static TorchSharp.torch;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [DefaultProperty("Device")]
    public class ToDevice
    {
        [XmlIgnore]
        public Device Device { get; set; }

        public IObservable<Tensor> Process(IObservable<Tensor> source)
        {
            return source.Select(tensor => {
                return tensor.to(Device);
            });
        }
    }
}