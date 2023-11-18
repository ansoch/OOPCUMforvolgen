using CosmoSim.Model.Galaxy;
using CosmoSim.Model.Galaxy.Entities;

namespace CosmoSim {
    public partial class CosmoSimForm : Form {
        private Label _spaceshipCell;
        private TextBox _textBox;

        private Image _spaceshipImage;
        private Image _starImage;
        private Image _asteroidImage;
        private Image _planetImage;
        private PictureBox _pictureBox;

        private Panel infoPanel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        private Panel shipConfigurator;
        private ComboBox comboBox;

        const int cellSize = 25;
        const int numColumns = 53;
        const int numRows = 40;

        private int clickedCellX;
        private int clickedCellY;

        private bool isClicked = false;

        public CosmoSimForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _spaceshipImage = Image.FromFile("SpaceShip.png");
            _planetImage = Image.FromFile("Planet.png");
            _starImage = Image.FromFile("Star.png");
            _asteroidImage = Image.FromFile("asteroid.png");

            Menu.Show();
            Menu.Controls.Add(new Button());
        }

        private void CosmoSimForm_Load(object sender, EventArgs e)
        {

            Panel panel = CreatePanel(numColumns, numRows, cellSize);
            // AddCellsToPanel(panel, numColumns, numRows, cellSize);
            CreateTable();
            panel.Controls.Add(_pictureBox);


            Panel newPanel = CreateNewPanel();
            PictureBox pictureBox = CreatePictureBox();

            newPanel.Controls.Add(pictureBox);

            CreateInfoPanel();

            shipConfigurator = CreateShipConfigurator();

            Controls.Add(shipConfigurator);
            Controls.Add(infoPanel);
            Controls.Add(newPanel);
            Controls.Add(panel);

            _textBox = CreateTextBox();
            Controls.Add(_textBox);
        }

        private Panel CreatePanel(int numColumns, int numRows, int cellSize)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                Size = new Size(numColumns * cellSize, numRows * cellSize)
            };

            return panel;
        }

        private void CreateInfoPanel()
        {
            infoPanel = new Panel();
            infoPanel.BackColor = System.Drawing.Color.LightGray;
            infoPanel.Size = new System.Drawing.Size(200, 300);
            infoPanel.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - infoPanel.Width,
                                                         Screen.PrimaryScreen.WorkingArea.Height - infoPanel.Height);

            label1 = new Label();
            label1.Text = "Еда: " + Player.SpaceShip.GetResource(Resource.food);
            label1.Location = new System.Drawing.Point(10, 10);
            infoPanel.Controls.Add(label1);

            label2 = new Label();
            label2.Text = "Топливо: " + Player.SpaceShip.GetResource(Resource.fuel);
            label2.Location = new System.Drawing.Point(10, 30);
            infoPanel.Controls.Add(label2);

            label3 = new Label();
            label3.Text = "Деньги: " + Player.SpaceShip.GetResource(Resource.money);
            label3.Location = new System.Drawing.Point(10, 50);
            infoPanel.Controls.Add(label3);

            label4 = new Label();
            label4.Text = "Металл: " + Player.SpaceShip.GetResource(Resource.metall);
            label4.Location = new System.Drawing.Point(10, 70);
            infoPanel.Controls.Add(label4);
        }
        private void UpdateInfoPanel()
        {
            label1.Text = "Food: " + Player.SpaceShip.GetResource(Resource.food);
            label2.Text = "Fuel: " + Player.SpaceShip.GetResource(Resource.fuel);
            label3.Text = "Money: " + Player.SpaceShip.GetResource(Resource.money);
            label4.Text = "Metal: " + Player.SpaceShip.GetResource(Resource.metall);
        }

        private Panel CreateShipConfigurator()
        {
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(ClientSize.Width - 200, ClientSize.Height - 150);
            panel.Size = new System.Drawing.Size(200, 100);
            Controls.Add(panel);

            comboBox = new ComboBox();
            comboBox.Items.AddRange(new string[] { "Classic", "Diagonal", "Knight", "Queen" });
            comboBox.Location = new System.Drawing.Point(0, 0);
            panel.Controls.Add(comboBox);

            Button applyButton = new Button();
            applyButton.Text = "Apply changes";
            applyButton.Location = new System.Drawing.Point(0, comboBox.Bottom + 10);
            applyButton.Click += ApplyButton_Click;
            panel.Controls.Add(applyButton);

            return panel;
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            string selectedOption = comboBox.SelectedItem as string;
            if (selectedOption != null)
            {
                switch (selectedOption)
                {
                    case "Classic": Player.SpaceShip.ChangeEngine(new ClassicEngine()); break;
                    case "Knight": Player.SpaceShip.ChangeEngine(new HorseEngine()); break;
                    case "Diagonal": Player.SpaceShip.ChangeEngine(new DiagonaleEngine()); break;
                    case "Queen": Player.SpaceShip.ChangeEngine(new QueenEngine()); break;
                }
            }
        }

        private void CreateTable()
        {
            _pictureBox = new PictureBox { Size = new Size(1350, 1020) };
            _pictureBox.BackColor = Color.Black;
            _pictureBox.Paint += PictureBoxTable_Paint;
            _pictureBox.MouseClick += PictureBoxTable_MouseClick;
        }
        private void PictureBoxTable_Paint(object sender, PaintEventArgs e)
        {
            int rows = numRows;
            int columns = numColumns;

            int cellWidth = cellSize;
            int cellHeight = cellSize;

            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Gray);

            for (int i = 0; i <= rows; i++)
            {
                int y = i * cellHeight;

                int maxY = _pictureBox.Height - 1;

                if (y <= maxY)
                    graphics.DrawLine(pen, 0, y, _pictureBox.Width, y);
            }
            for (int j = 0; j <= columns; j++)
            {
                int x = j * cellWidth;

                int maxX = _pictureBox.Width - 1;

                if (x <= maxX)
                    graphics.DrawLine(pen, x, 0, x, _pictureBox.Height);
            }

            if (clickedCellX >= 0 && clickedCellY >= 0 && isClicked)
            {
                isClicked = false;
                int imageX = clickedCellX * cellWidth;
                int imageY = clickedCellY * cellHeight;

                //graphics.DrawImage(_spaceshipImage, imageX, imageY, cellWidth, cellHeight);
                bool moved = Player.SpaceShip.Fly(clickedCellX, clickedCellY);
                if (!moved) MessageBox.Show("Move not possible");
                UpdateInfoPanel();  
            }
            RenderSystem(graphics);

        }
        private void PictureBoxTable_MouseClick(object sender, MouseEventArgs e)
        {
            int cellX = e.X / cellSize;
            int cellY = e.Y / cellSize;

            clickedCellX = cellX;
            clickedCellY = cellY;

            isClicked = true;

            _pictureBox.Invalidate();
            //MessageBox.Show(cellX.ToString() + " " + cellY.ToString());

            infoPanel.Invalidate();
        }


        private void AddCellsToPanel(Panel panel, int numColumns, int numRows, int cellSize)
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    Label cell = CreateCell(col, row, cellSize);
                    panel.Controls.Add(cell);

                    if (row != 0 || col != 0) continue;
                    _spaceshipCell = cell;
                    AddSpaceshipImageToCell(cell);
                }
            }
        }

        private Label CreateCell(int col, int row, int cellSize)
        {
            Label cell = new Label
            {
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Location = new Point(col * cellSize, row * cellSize),
                Size = new Size(cellSize, cellSize),
                Tag = $"{col};{row}"
            };
            cell.Click += Cell_Click;
            
            return cell;
        }

        private Panel CreateNewPanel()
        {
            Panel newPanel = new Panel
            {
                Size = new Size(550, 500),
                Location = new Point(ClientSize.Width - 550, 0),
                BorderStyle = BorderStyle.FixedSingle
            };

            return newPanel;
        }

        private PictureBox CreatePictureBox()
        {
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(550, 500),
                BackColor = Color.Black
            };
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseDown += PictureBox_MouseDown;

            return pictureBox;
        }

        private TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill
            };

            return textBox;
        }

        private void AddSpaceshipImageToCell(Control cell)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = _spaceshipImage;
            pictureBox.Dock = DockStyle.Fill;
            cell.Controls.Add(pictureBox);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            const int starSystemRadius = 5;
            const int planetRadius = 3;
            const int meteorClusterRadius = 2;
            
            Color starSystemColor = Color.Blue;
            Color planetColor = Color.Green;
            Color meteorClusterColor = Color.Red;

            foreach (StarSystem starSystem in Galaxy.StarSystems)
            {
                int centerX = starSystem.Coordinates.X;
                int centerY = starSystem.Coordinates.Y;

                int x = centerX - starSystemRadius;
                int y = centerY - starSystemRadius;
                int diameter = starSystemRadius * 2;

                Rectangle circleBounds = new Rectangle(x, y, diameter, diameter);
                e.Graphics.FillEllipse(new SolidBrush(starSystemColor), circleBounds);

                PictureBox pictureBox = (PictureBox)sender;
                Point mousePosition = pictureBox.PointToClient(MousePosition);

                if (circleBounds.Contains(mousePosition))
                {
                    MessageBox.Show(starSystem.Name.ToString());
                    Player.SpaceShip.InToTheStarSystem(starSystem);
                    _pictureBox.Invalidate();
                }
                
                foreach (Planet planet in starSystem.Planets)
                {
                    int planetX = centerX + planet.Coordinates.X;
                    int planetY = centerY + planet.Coordinates.Y;
                    DrawEllipse(e.Graphics, planetColor, planetX, planetY, planetRadius);

                    Rectangle planetBounds = new Rectangle(planetX - planetRadius, planetY - planetRadius, planetRadius * 2, planetRadius * 2);

                    if (planetBounds.Contains(mousePosition))
                    {
                        MessageBox.Show($"Planet - Coordinated: {planet.Coordinates.X}, {planet.Coordinates.Y}, Resources: " +
                                        $"{planet.Resources.Energy.Size}, {planet.Resources.Metal.Size}, {planet.Resources.Water.Size}");
                    }
                }

                foreach (MeteorCluster cluster in starSystem.MeteorClusters)
                {
                    int clusterX = centerX + cluster.Coordinates.X;
                    int clusterY = centerY + cluster.Coordinates.Y;
                    DrawEllipse(e.Graphics, meteorClusterColor, clusterX, clusterY, meteorClusterRadius);

                    Rectangle clusterBounds = new Rectangle(clusterX - meteorClusterRadius, clusterY - meteorClusterRadius, meteorClusterRadius * 2, meteorClusterRadius * 2);

                    if (clusterBounds.Contains(mousePosition))
                    {
                        MessageBox.Show($"Meteor Cluster - Coordinated: {cluster.Coordinates.X}, {cluster.Coordinates.Y}");
                    }
                }
            }
            
        }
        private void DrawEllipse(Graphics graphics, Color color, int centerX, int centerY, int radius)
        {
            int x = centerX - radius;
            int y = centerY - radius;
            int diameter = radius * 2;

            Rectangle circleBounds = new Rectangle(x, y, diameter, diameter);
            graphics.FillEllipse(new SolidBrush(color), circleBounds);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pictureBox = (PictureBox)sender;
                pictureBox.Invalidate();
            }
        }
        

        private void Cell_Click(object sender, EventArgs e)
        {
            Coordinates currentCoordinates = Player.SpaceShip.GetCoordinates();

            Label cell = (Label)sender;
            int[] arrayCoordinates = cell.Tag.ToString().Split(';').Select(int.Parse).ToArray();

            Coordinates newCoordinates = new Coordinates(arrayCoordinates[0], arrayCoordinates[1]);
            
            ReplaceSpaceshipImage(cell, newCoordinates);
            
        }

        private void ReplaceSpaceshipImage(Label cell, Coordinates newCoordinates)
        {

            Graphics graphics = Graphics.FromImage(_spaceshipImage);
            graphics.Dispose();

            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = _spaceshipImage;

            pictureBox.Dock = DockStyle.Fill;
            cell.Controls.Add(pictureBox);

            _spaceshipCell.Controls.Clear();
            _spaceshipCell = cell;

            Player.SpaceShip.ChangePosition(newCoordinates.X, newCoordinates.Y);
        }

        private void RenderSystem(Graphics graphics)
        {
            StarSystem starSystem = Player.SpaceShip.CurrentSystem;
            graphics.DrawImage(_starImage, 27*cellSize, 20*cellSize, cellSize, cellSize);

            foreach (Planet planet in starSystem.Planets)
            {
                Coordinates coordinates = TransformCoordinates(planet.Coordinates);
                graphics.DrawImage(_planetImage, coordinates.X * cellSize, coordinates.Y * cellSize, cellSize, cellSize);
                //Console.WriteLine(coordinates.X);
            }
            foreach (MeteorCluster cluster in starSystem.MeteorClusters)
            {
                Coordinates coordinates = TransformCoordinates(cluster.Coordinates);
                graphics.DrawImage(_asteroidImage, coordinates.X * cellSize, coordinates.Y * cellSize, cellSize, cellSize);
            }

            Coordinates playerCoordinates = Player.SpaceShip.GetCoordinates();
            graphics.DrawImage(_spaceshipImage, playerCoordinates.X * cellSize, playerCoordinates.Y * cellSize, cellSize, cellSize);
        }
        public static Coordinates TransformCoordinates(Coordinates coordinates)
        {
            int newX = coordinates.X + 27;
            int newY = coordinates.Y +20;
            return new Coordinates(newX, newY);
        }
    }
}
