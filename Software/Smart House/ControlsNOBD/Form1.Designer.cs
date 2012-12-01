namespace ControlsNOBD
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b_connect_to_db = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connect_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainTS = new System.Windows.Forms.ToolStrip();
            this.b_SimpleDatchik = new System.Windows.Forms.ToolStripButton();
            this.tExDatcik = new System.Windows.Forms.ToolStripButton();
            this.bUstr = new System.Windows.Forms.ToolStripButton();
            this.bDimmer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bn = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.b_commit = new System.Windows.Forms.ToolStripButton();
            this.gw = new System.Windows.Forms.DataGridView();
            this.LDatabase = new System.Windows.Forms.ToolStripLabel();
            this.bAdd = new System.Windows.Forms.ToolStripButton();
            this.bMod = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lDatchik = new System.Windows.Forms.ToolStripLabel();
            this.bGenMess = new System.Windows.Forms.ToolStripButton();
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.MainTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bn)).BeginInit();
            this.bn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.базаДанныхToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(856, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menu";
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b_connect_to_db});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.базаДанныхToolStripMenuItem.Text = "База данных";
            // 
            // b_connect_to_db
            // 
            this.b_connect_to_db.Name = "b_connect_to_db";
            this.b_connect_to_db.Size = new System.Drawing.Size(156, 22);
            this.b_connect_to_db.Text = "Подключиться";
            this.b_connect_to_db.Click += new System.EventHandler(this.b_connect_to_db_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connect_status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(856, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connect_status
            // 
            this.connect_status.Name = "connect_status";
            this.connect_status.Size = new System.Drawing.Size(0, 17);
            // 
            // MainTS
            // 
            this.MainTS.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b_SimpleDatchik,
            this.tExDatcik,
            this.bUstr,
            this.bDimmer,
            this.toolStripSeparator1,
            this.LDatabase,
            this.bAdd,
            this.bMod,
            this.toolStripSeparator2,
            this.lDatchik,
            this.bGenMess});
            this.MainTS.Location = new System.Drawing.Point(0, 24);
            this.MainTS.Name = "MainTS";
            this.MainTS.Size = new System.Drawing.Size(250, 315);
            this.MainTS.TabIndex = 2;
            // 
            // b_SimpleDatchik
            // 
            this.b_SimpleDatchik.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.b_SimpleDatchik.Image = ((System.Drawing.Image)(resources.GetObject("b_SimpleDatchik.Image")));
            this.b_SimpleDatchik.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.b_SimpleDatchik.Name = "b_SimpleDatchik";
            this.b_SimpleDatchik.Size = new System.Drawing.Size(247, 19);
            this.b_SimpleDatchik.Text = "Простые датчики";
            this.b_SimpleDatchik.Click += new System.EventHandler(this.b_SimpleDatchik_Click);
            // 
            // tExDatcik
            // 
            this.tExDatcik.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tExDatcik.Image = ((System.Drawing.Image)(resources.GetObject("tExDatcik.Image")));
            this.tExDatcik.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tExDatcik.Name = "tExDatcik";
            this.tExDatcik.Size = new System.Drawing.Size(247, 19);
            this.tExDatcik.Text = "Датчики более чем с 2-мя состояниями";
            this.tExDatcik.Click += new System.EventHandler(this.tExDatcik_Click);
            // 
            // bUstr
            // 
            this.bUstr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bUstr.Image = ((System.Drawing.Image)(resources.GetObject("bUstr.Image")));
            this.bUstr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bUstr.Name = "bUstr";
            this.bUstr.Size = new System.Drawing.Size(247, 19);
            this.bUstr.Text = "Устройства";
            this.bUstr.Click += new System.EventHandler(this.bUstr_Click);
            // 
            // bDimmer
            // 
            this.bDimmer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bDimmer.Image = ((System.Drawing.Image)(resources.GetObject("bDimmer.Image")));
            this.bDimmer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bDimmer.Name = "bDimmer";
            this.bDimmer.Size = new System.Drawing.Size(247, 19);
            this.bDimmer.Text = "Диммеры";
            this.bDimmer.Click += new System.EventHandler(this.bDimmer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(247, 6);
            // 
            // bn
            // 
            this.bn.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bn.CountItem = this.bindingNavigatorCountItem;
            this.bn.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.b_commit});
            this.bn.Location = new System.Drawing.Point(250, 24);
            this.bn.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bn.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bn.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bn.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bn.Name = "bn";
            this.bn.PositionItem = this.bindingNavigatorPositionItem;
            this.bn.Size = new System.Drawing.Size(606, 25);
            this.bn.TabIndex = 3;
            this.bn.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Добавить";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 22);
            this.bindingNavigatorCountItem.Text = "для {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Удалить";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // b_commit
            // 
            this.b_commit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.b_commit.Image = ((System.Drawing.Image)(resources.GetObject("b_commit.Image")));
            this.b_commit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.b_commit.Name = "b_commit";
            this.b_commit.Size = new System.Drawing.Size(74, 22);
            this.b_commit.Text = "Применить";
            this.b_commit.Click += new System.EventHandler(this.b_commit_Click);
            // 
            // gw
            // 
            this.gw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gw.Location = new System.Drawing.Point(250, 49);
            this.gw.Name = "gw";
            this.gw.Size = new System.Drawing.Size(606, 290);
            this.gw.TabIndex = 4;
            // 
            // LDatabase
            // 
            this.LDatabase.Name = "LDatabase";
            this.LDatabase.Size = new System.Drawing.Size(247, 15);
            this.LDatabase.Text = "Добавление удаление изменение устройств";
            // 
            // bAdd
            // 
            this.bAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bAdd.Image = ((System.Drawing.Image)(resources.GetObject("bAdd.Image")));
            this.bAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(247, 19);
            this.bAdd.Text = "Подключить новое устройство";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bMod
            // 
            this.bMod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bMod.Image = ((System.Drawing.Image)(resources.GetObject("bMod.Image")));
            this.bMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMod.Name = "bMod";
            this.bMod.Size = new System.Drawing.Size(247, 19);
            this.bMod.Text = "Переключить уст-ва или выключить";
            this.bMod.Click += new System.EventHandler(this.bMod_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(247, 6);
            // 
            // lDatchik
            // 
            this.lDatchik.Name = "lDatchik";
            this.lDatchik.Size = new System.Drawing.Size(247, 15);
            this.lDatchik.Text = "Работа с простыми датчиками";
            // 
            // bGenMess
            // 
            this.bGenMess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bGenMess.Image = ((System.Drawing.Image)(resources.GetObject("bGenMess.Image")));
            this.bGenMess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bGenMess.Name = "bGenMess";
            this.bGenMess.Size = new System.Drawing.Size(247, 19);
            this.bGenMess.Text = "Срабатывания датчика";
            this.bGenMess.Click += new System.EventHandler(this.bGenMess_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 361);
            this.Controls.Add(this.gw);
            this.Controls.Add(this.bn);
            this.Controls.Add(this.MainTS);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Контролер";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MainTS.ResumeLayout(false);
            this.MainTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bn)).EndInit();
            this.bn.ResumeLayout(false);
            this.bn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b_connect_to_db;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connect_status;
        private System.Windows.Forms.ToolStrip MainTS;
        private System.Windows.Forms.ToolStripButton b_SimpleDatchik;
        private System.Windows.Forms.ToolStripButton tExDatcik;
        private System.Windows.Forms.ToolStripButton bUstr;
        private System.Windows.Forms.ToolStripButton bDimmer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.BindingNavigator bn;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView gw;
        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.ToolStripButton b_commit;
        private System.Windows.Forms.ToolStripLabel LDatabase;
        private System.Windows.Forms.ToolStripButton bAdd;
        private System.Windows.Forms.ToolStripButton bMod;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lDatchik;
        private System.Windows.Forms.ToolStripButton bGenMess;


    }
}

