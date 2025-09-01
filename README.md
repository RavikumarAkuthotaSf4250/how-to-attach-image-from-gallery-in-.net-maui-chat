# how-to-attach-image-from-gallery-in-.net-maui-chat

## Sample

```xaml

    this.chipGroup.ChipClicked += ChipGroup_ChipClicked;

    private async void ChipGroup_ChipClicked(object? sender, EventArgs e)
    {
        if (sender is SfChip chip && chip.Children[0] is View child)
        {
            var model = child.BindingContext as Model;
            if (model != null)
            {
                if (model.Text == "Image")
                {
                    var result = await FilePicker.Default.PickAsync(new PickOptions
                    {
                        PickerTitle = "Select an Image",
                        FileTypes = FilePickerFileType.Images,
                    });

                    if (result != null && _imageDb != null)
                    {
                        var stream = await result.OpenReadAsync();
                        var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        var image = new ImageModel { Id = count, ImageData = imageBytes };
                        await _imageDb.SaveImageAsync(image);
                        UpdateImage();
                        count++;
                    }
                }
                else
                {
                    if (this.popup != null && this.popup.IsOpen)
                    {
                        this.popup.IsOpen = false;
                    }
                 }
            }
        }
    }

    private async void UpdateImage()
    {
        if (_imageDb != null)
        {
            var image = await _imageDb.GetImageByIdAsync(count);
            if (image != null && image.ImageData != null && this.viewModel != null && this.popup != null)
            {
                this.viewModel.Messages.Add(new ImageMessage()
                {
                     Author = this.viewModel.CurrentUser,
                    Source = ImageSource.FromStream(() => new MemoryStream(image.ImageData)),
                });

                if (this.popup.IsOpen)
                {
                    this.popup.IsOpen = false;
                }
            }
        }
    }

```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.

