using static TorchSharp.torch;

namespace MidasTorchModel
{
    public static class Utils
    {
        public static Tensor NormalizeImageTensor(Tensor tensor)
        {
            return (tensor - tensor.min()) / (tensor.max() - tensor.min()) * 255;
        }
    }
}
