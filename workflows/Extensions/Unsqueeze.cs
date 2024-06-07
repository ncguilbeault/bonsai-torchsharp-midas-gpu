using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using static TorchSharp.torch;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [DefaultProperty("Dimension")]
    public class Unsqueeze
    {
        public long Dimension { get; set; } = 0;
        public IObservable<Tensor> Process(IObservable<Tensor> source)
        {
            TorchSharpInitializer.Initialize();

            return source.Select(tensor => {
                return tensor.unsqueeze(Dimension);
            });
        }
    }
}
