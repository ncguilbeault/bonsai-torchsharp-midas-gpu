import torch
import exportsd

model_type = "DPT_Large"
midas = torch.hub.load("intel-isl/MiDaS", model_type, pretrained=True)
midas.eval()

with open(f"{model_type}.dat", "wb") as f:
    exportsd.save_state_dict(midas.to("cpu").state_dict(), f)

# example_input = torch.rand(1, 3, 256, 256)

# with torch.no_grad():
#     o1 = midas(example_input)
#     traced_model = torch.jit.trace(midas, example_input)
#     traced_model.save("models/DPT_Large.pt")
