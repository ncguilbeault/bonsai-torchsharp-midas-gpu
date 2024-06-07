using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using static TorchSharp.torch;
using static TorchSharp.torchvision;
using System.Xml.Serialization;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class TransformInput
    {       
        private ITransform inputTransform;

        public IObservable<Tensor> Process(IObservable<Tensor> source)
        {
            double[] means = new double[] {0.485f, 0.456f, 0.406f};
            double[] stds = new double[] {0.229f, 0.224f, 0.225f};

            inputTransform = transforms.Compose(
                transforms.Resize(256, 256),
                transforms.Normalize(means, stds)
            );

            return source.Select(tensor => {
                return inputTransform.call(tensor);
            });
        }
    }
}