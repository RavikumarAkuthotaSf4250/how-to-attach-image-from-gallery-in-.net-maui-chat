
using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiChat
{
    public class ContentPageBehavior : Behavior<ContentPage>
    {
        #region Fields

        private SfChat? chat;
        private SfPopup? popup;
        private SfChipGroup? chipGroup;
        private ImageDatabase? _imageDb = IPlatformApplication.Current!.Services.GetService<ImageDatabase>();
        private int count;
        private ChatViewModel? viewModel;

        #endregion

        #region Protected Methods

        protected override void OnAttachedTo(ContentPage bindable)
        {
            this.chat = bindable.FindByName<SfChat>("sfChat");
            this.viewModel = bindable.BindingContext as ChatViewModel;
            if (chat != null)
            {
                chat.AttachmentButtonClicked += Chat_AttachmentButtonClicked;
            }
            _imageDb?.RefreshDataBaseTable();
            count = 1;
            this.popup = bindable.Resources["sfPopup"] as SfPopup;
            this.chipGroup = bindable.Resources["chipGroup"] as SfChipGroup;
            if (this.chipGroup != null)
            {
                this.chipGroup.ChipClicked += ChipGroup_ChipClicked;
            }

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            if (this.chat != null)
            {
                this.chat.AttachmentButtonClicked -= Chat_AttachmentButtonClicked;
                this.chat = null;
            }
            if (chipGroup != null)
            {
                this.chipGroup.ChipClicked -= ChipGroup_ChipClicked;
                this.chipGroup = null;
            }
            this.viewModel = null;
            this.popup = null;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Callbacks

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
                        if (this.popup != null)
                        {
                            this.popup!.IsOpen = false;
                        }
                    }
                }
            }
        }

        private void Chat_AttachmentButtonClicked(object? sender, EventArgs e)
        {
            if (this.popup != null && this.chat != null)
            {
                this.popup.RelativeView = this.chat.Editor;
                this.popup.RelativePosition = PopupRelativePosition.AlignTopRight;
                this.popup.IsOpen = true;
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

                    this.popup.IsOpen = false;
                }
            }
        }
        #endregion
    }
}
