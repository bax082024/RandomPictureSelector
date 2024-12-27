using RandomPictureSelector.UI;
using System.Diagnostics;

namespace RandomPictureSelector
{
    public partial class RandomForm : Form
    {
        private List<string> imagePaths = new List<string>(); // Paths for listBox1
        private List<string> usedImagePaths = new List<string>(); // Paths for listBox2
        private Random random = new Random(); // For random selection

        // Shuffle
        private int shuffleIndex = 0; // Tracks the current image being shown
        private int shuffleCount = 0; // Counts how many times images have been shuffled
        private int customShuffleSpeed = 100;// How many images to show during shuffle
        private int maxShuffleCount;

        private const string SettingsFilePath = "UserSettings.json";

        private string currentTheme = "Light";




        public RandomForm()
        {
            InitializeComponent();
            shuffleTimer.Tick += shuffleTimer_Tick;

            SaveSettings();
            currentTheme = "DesignView";
            ApplyTheme(currentTheme);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        if (!imagePaths.Contains(filePath))
                        {
                            imagePaths.Add(filePath);
                            listBox1.Items.Add(System.IO.Path.GetFileName(filePath)); // Add filename to listBox1
                        }
                    }
                }
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("Please add images first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Reset shuffle variables
            shuffleIndex = 0;
            shuffleCount = 0;

            // Dynamically set MaxShuffleCount
            maxShuffleCount = Math.Max(20, Math.Min(imagePaths.Count, shuffleProgressBar.Maximum)); // Minimum 20 or the number of images
            shuffleProgressBar.Maximum = maxShuffleCount;

            // Reset progress bar
            ResetProgressBar(maxShuffleCount);

            // Update the shuffle label
            lblShuffleStatus.Text = $"Shuffling... 0/{maxShuffleCount}";

            // Show the first image immediately
            if (imagePaths.Count > 0)
            {
                pictureBox1.Image = FixImageOrientation(LoadImageSafely(imagePaths[shuffleIndex]));
                shuffleProgressBar.Value = 1; // Start progress bar at 1
            }

            // Start the shuffle timer
            shuffleTimer.Interval = customShuffleSpeed;
            shuffleTimer.Start();
        }





        private void btnMove_Click(object sender, EventArgs e)
        {
            if (usedImagePaths.Count == 0)
            {
                MessageBox.Show("No images to move back!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string filePath in usedImagePaths)
            {
                imagePaths.Add(filePath);
                listBox1.Items.Add(System.IO.Path.GetFileName(filePath));
            }

            usedImagePaths.Clear();
            listBox2.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Image FixImageOrientation(Image img)
        {
            const int ExifOrientationId = 0x112; // EXIF tag for Orientation

            try
            {
                if (img.PropertyIdList.Contains(ExifOrientationId))
                {
                    var prop = img.GetPropertyItem(ExifOrientationId);

                    if (prop?.Value != null)
                    {
                        int orientation = BitConverter.ToUInt16(prop.Value, 0);

                        // Apply necessary rotation based on orientation value
                        switch (orientation)
                        {
                            case 3: // 180 degrees
                                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 6: // 90 degrees clockwise
                                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 8: // 90 degrees counterclockwise
                                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }

                        // Remove the EXIF orientation tag to prevent reapplying rotation
                        img.RemovePropertyItem(ExifOrientationId);
                    }
                }
            }
            catch
            {
                // If something goes wrong, we just return the image as is.
            }

            return img;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the data being dragged is a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // Allow copying
            }
            else
            {
                e.Effect = DragDropEffects.None; // Disallow if not files
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                // Check if the file is an image
                if (file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".bmp"))
                {
                    if (!imagePaths.Contains(file))
                    {
                        imagePaths.Add(file);
                        listBox1.Items.Add(System.IO.Path.GetFileName(file));
                    }
                }
                else
                {
                    MessageBox.Show($"The file '{System.IO.Path.GetFileName(file)}' is not a supported image type.",
                                    "Invalid File",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                saveFileDialog.Title = "Save Image Lists";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Save listBox1 (Insert Images)
                        writer.WriteLine("[Insert Images]");
                        foreach (string path in imagePaths)
                        {
                            writer.WriteLine(path);
                        }

                        // Save listBox2 (Used Images)
                        writer.WriteLine("[Used Images]");
                        foreach (string path in usedImagePaths)
                        {
                            writer.WriteLine(path);
                        }
                    }

                    MessageBox.Show("Image lists saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.txt";
                openFileDialog.Title = "Load Image Lists";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePaths.Clear();
                    usedImagePaths.Clear();
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();

                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        string line;
                        bool loadingInsertImages = false;
                        bool loadingUsedImages = false;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line == "[Insert Images]")
                            {
                                loadingInsertImages = true;
                                loadingUsedImages = false;
                                continue;
                            }

                            if (line == "[Used Images]")
                            {
                                loadingInsertImages = false;
                                loadingUsedImages = true;
                                continue;
                            }

                            if (loadingInsertImages)
                            {
                                if (System.IO.File.Exists(line))
                                {
                                    imagePaths.Add(line);
                                    listBox1.Items.Add(System.IO.Path.GetFileName(line));
                                }
                            }
                            else if (loadingUsedImages)
                            {
                                if (System.IO.File.Exists(line))
                                {
                                    usedImagePaths.Add(line);
                                    listBox2.Items.Add(System.IO.Path.GetFileName(line));
                                }
                            }
                        }
                    }

                    MessageBox.Show("Image lists loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void shuffleTimer_Tick(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                shuffleTimer.Stop();
                shuffleProgressBar.Visible = false;
                lblShuffleStatus.Text = "No images to shuffle!";
                return;
            }

            // Display the next image in the shuffle
            pictureBox1.Image = FixImageOrientation(LoadImageSafely(imagePaths[shuffleIndex]));

            // Cycle through the images
            shuffleIndex = (shuffleIndex + 1) % imagePaths.Count;

            // Increment shuffle counter and update progress bar
            shuffleCount++;
            shuffleProgressBar.Value = Math.Min(shuffleCount, maxShuffleCount);
            lblShuffleStatus.Text = $"Shuffling... {shuffleCount}/{maxShuffleCount}";

            // Stop after reaching the maximum shuffle count
            if (shuffleCount >= maxShuffleCount)
            {
                shuffleTimer.Stop();
                shuffleProgressBar.Value = shuffleProgressBar.Maximum;
                shuffleProgressBar.Visible = false;
                lblShuffleStatus.Text = "Shuffle complete!";
                SelectFinalRandomImage();
            }
        }









        private void SelectFinalRandomImage()
        {
            if (imagePaths.Count == 0)
            {
                lblShuffleStatus.Text = "No images to display!";
                return;
            }

            // Select a random image
            int randomIndex = random.Next(imagePaths.Count);
            string selectedImage = imagePaths[randomIndex];

            // Display the selected image in the PictureBox, fixing its orientation
            pictureBox1.Image = FixImageOrientation(Image.FromFile(selectedImage));

            // Move the selected image to "Used Images"
            usedImagePaths.Add(selectedImage);
            listBox2.Items.Add(System.IO.Path.GetFileName(selectedImage));

            imagePaths.RemoveAt(randomIndex);
            listBox1.Items.RemoveAt(randomIndex);

            lblShuffleStatus.Text = "Final image selected!";
        }


        private Image LoadImageSafely(string filePath)
        {
            try
            {
                return Image.FromFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void shuffleSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                // Pass current values to the SettingsForm
                settingsForm.MinShuffleCount = shuffleProgressBar.Minimum;
                settingsForm.ShuffleSpeed = customShuffleSpeed;

                // Show the SettingsForm
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve updated settings with validation
                    if (settingsForm.MinShuffleCount >= 1 && settingsForm.ShuffleSpeed > 0)
                    {
                        shuffleProgressBar.Minimum = settingsForm.MinShuffleCount;
                        customShuffleSpeed = settingsForm.ShuffleSpeed;
                    }
                    else
                    {
                        MessageBox.Show("Invalid settings. Please ensure values are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void ResetProgressBar(int calculatedShuffleCount)
        {
            shuffleProgressBar.Minimum = 0;
            shuffleProgressBar.Maximum = calculatedShuffleCount;
            shuffleProgressBar.Value = 0;
            shuffleProgressBar.Visible = true;
        }

        private void SaveSettings()
        {
            try
            {
                var settings = new UserSettings
                {
                    MinShuffleCount = shuffleProgressBar.Minimum,
                    ShuffleSpeed = customShuffleSpeed,
                    SelectedTheme = currentTheme
                };

                string json = System.Text.Json.JsonSerializer.Serialize(settings, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true // Make the JSON more readable
                });

                File.WriteAllText(SettingsFilePath, json);
                MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var settings = System.Text.Json.JsonSerializer.Deserialize<UserSettings>(json);

                    if (settings != null)
                    {
                        shuffleProgressBar.Minimum = settings.MinShuffleCount;
                        customShuffleSpeed = settings.ShuffleSpeed;
                        currentTheme = settings.SelectedTheme;
                        ApplyTheme(currentTheme);
                        MessageBox.Show("Settings loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No settings file found. Default settings will be used.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ApplyTheme(string theme)
        {
            this.Paint -= RandomForm_Paint; // Detach any existing Paint event to prevent conflicts

            switch (theme)
            {
                case "Light":
                    this.BackColor = Color.White;
                    foreach (Control control in this.Controls)
                    {
                        control.BackColor = Color.White;
                        control.ForeColor = Color.Black;
                    }
                    break;

                case "Dark":
                    this.BackColor = Color.Black;
                    foreach (Control control in this.Controls)
                    {
                        control.BackColor = Color.Black;
                        control.ForeColor = Color.White;
                    }
                    break;

                case "Gradient":
                    this.Paint += RandomForm_Paint; // Attach the Paint event for the gradient background
                    foreach (Control control in this.Controls)
                    {
                        control.BackColor = Color.Transparent; // Ensure controls are transparent for gradient
                        control.ForeColor = Color.White;
                        if (control is Button button)
                        {
                            button.BackColor = Color.DimGray;
                            button.FlatStyle = FlatStyle.Flat; // Optional: Flat style for modern look
                        }
                    }
                    break;

                case "DesignView":
                default:
                    this.BackColor = Color.LightYellow;
                    foreach (Control control in this.Controls)
                    {
                        control.BackColor = Color.LightYellow;
                        control.ForeColor = Color.Black;
                    }
                    break;
            }

            // Apply theme to menu strip separately
            menuStrip1.BackColor = theme == "Dark" ? Color.Gray : Color.Beige;
            menuStrip1.ForeColor = theme == "Dark" ? Color.White : Color.Black;
        }

        private void RandomForm_Paint(object sender, PaintEventArgs e)
        {
            var gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                this.ClientRectangle,
                Color.LightBlue,
                Color.DarkBlue,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }


        private void ChangeTheme(string theme)
        {
            currentTheme = theme; // Update the current theme
            ApplyTheme(theme); // Apply the selected theme
            SaveSettings(); // Save the selected theme in settings
        }

        private void saveSettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void RandomForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Light");
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Dark");
        }

        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Gradient");
        }
    }
}
