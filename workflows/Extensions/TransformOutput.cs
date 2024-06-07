using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using static TorchSharp.torch;
using static TorchSharp.torchvision;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [DefaultProperty("OutputSize")]
    public class TransformOutput
    {
        private ITransform outputTransform;
        public OpenCV.Net.Size OutputSize { get; set; } = new OpenCV.Net.Size(0, 0);

        public IObservable<Tensor> Process(IObservable<Tensor> source)
        {

            outputTransform = transforms.Compose(
                transforms.Resize(OutputSize.Width, OutputSize.Height)
            );

            return source.Select(tensor => {
                return Utils.NormalizeImageTensor(outputTransform.call(tensor));
            });
        }
    }
}