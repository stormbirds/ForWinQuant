namespace ForWinQuant
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarRequest = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelAssetUpdateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewSubUsers = new System.Windows.Forms.TreeView();
            this.splitContainerMine = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxExecutionLog = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.RichTextBox();
            this.groupBoxDelegateList = new System.Windows.Forms.GroupBox();
            this.dataGridViewUserOrder = new System.Windows.Forms.DataGridView();
            this.orderRevert = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pairCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tradeCoinKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceCoinKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matched = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastModifiedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxAdvancedSettings = new System.Windows.Forms.GroupBox();
            this.SynchronizeAdvancedSettings = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxOfflinePrompt = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePickerLateHighPriceStart = new System.Windows.Forms.DateTimePicker();
            this.checkBoxTwoHigherPrices = new System.Windows.Forms.CheckBox();
            this.checkBoxEarlyHighPrice = new System.Windows.Forms.CheckBox();
            this.checkBoxLateHighPrice = new System.Windows.Forms.CheckBox();
            this.dateTimePickerTwoHigherPriceStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEarlyHighPriceStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerLateHighPriceEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEarlyHighPriceEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTwoHigherPriceEnd = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxDealPrompt = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDownRetainedAmount = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonMine = new System.Windows.Forms.Button();
            this.label_NGRC_value = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label_USDT_value = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelCountTradeToday = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelTodayRound = new System.Windows.Forms.Label();
            this.labelCurrentPrice = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.timerChart = new System.Windows.Forms.Timer(this.components);
            this.timerAction = new System.Windows.Forms.Timer(this.components);
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuMineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SynchronizeAdvancedSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMine)).BeginInit();
            this.splitContainerMine.Panel1.SuspendLayout();
            this.splitContainerMine.Panel2.SuspendLayout();
            this.splitContainerMine.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxExecutionLog.SuspendLayout();
            this.groupBoxDelegateList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserOrder)).BeginInit();
            this.groupBoxAdvancedSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRetainedAmount)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.mainStatusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // mainStatusStrip
            // 
            resources.ApplyResources(this.mainStatusStrip, "mainStatusStrip");
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelLogin,
            this.toolStripProgressBarRequest,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelAssetUpdateTime});
            this.mainStatusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainStatusStrip.Name = "mainStatusStrip";
            // 
            // toolStripStatusLabelLogin
            // 
            this.toolStripStatusLabelLogin.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelLogin.Name = "toolStripStatusLabelLogin";
            resources.ApplyResources(this.toolStripStatusLabelLogin, "toolStripStatusLabelLogin");
            // 
            // toolStripProgressBarRequest
            // 
            this.toolStripProgressBarRequest.Margin = new System.Windows.Forms.Padding(20, 3, 1, 3);
            this.toolStripProgressBarRequest.Name = "toolStripProgressBarRequest";
            resources.ApplyResources(this.toolStripProgressBarRequest, "toolStripProgressBarRequest");
            this.toolStripProgressBarRequest.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBarRequest.Tag = "初始化";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            resources.ApplyResources(this.toolStripStatusLabelStatus, "toolStripStatusLabelStatus");
            // 
            // toolStripStatusLabelAssetUpdateTime
            // 
            this.toolStripStatusLabelAssetUpdateTime.Name = "toolStripStatusLabelAssetUpdateTime";
            resources.ApplyResources(this.toolStripStatusLabelAssetUpdateTime, "toolStripStatusLabelAssetUpdateTime");
            this.toolStripStatusLabelAssetUpdateTime.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewSubUsers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainerMine);
            // 
            // treeViewSubUsers
            // 
            resources.ApplyResources(this.treeViewSubUsers, "treeViewSubUsers");
            this.treeViewSubUsers.Name = "treeViewSubUsers";
            // 
            // splitContainerMine
            // 
            resources.ApplyResources(this.splitContainerMine, "splitContainerMine");
            this.splitContainerMine.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMine.Name = "splitContainerMine";
            // 
            // splitContainerMine.Panel1
            // 
            this.splitContainerMine.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainerMine.Panel2
            // 
            this.splitContainerMine.Panel2.Controls.Add(this.groupBoxAction);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxExecutionLog, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxDelegateList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxAdvancedSettings, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxExecutionLog
            // 
            this.groupBoxExecutionLog.Controls.Add(this.textBoxLog);
            resources.ApplyResources(this.groupBoxExecutionLog, "groupBoxExecutionLog");
            this.groupBoxExecutionLog.Name = "groupBoxExecutionLog";
            this.groupBoxExecutionLog.TabStop = false;
            // 
            // textBoxLog
            // 
            resources.ApplyResources(this.textBoxLog, "textBoxLog");
            this.textBoxLog.HideSelection = false;
            this.textBoxLog.Name = "textBoxLog";
            // 
            // groupBoxDelegateList
            // 
            this.groupBoxDelegateList.Controls.Add(this.dataGridViewUserOrder);
            resources.ApplyResources(this.groupBoxDelegateList, "groupBoxDelegateList");
            this.groupBoxDelegateList.Name = "groupBoxDelegateList";
            this.groupBoxDelegateList.TabStop = false;
            // 
            // dataGridViewUserOrder
            // 
            this.dataGridViewUserOrder.AllowUserToAddRows = false;
            this.dataGridViewUserOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewUserOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderRevert,
            this.id,
            this.orderId,
            this.orderKey,
            this.memberId,
            this.memberKey,
            this.usr,
            this.clientId,
            this.orderType,
            this.pairCode,
            this.tradeCoinKey,
            this.priceCoinKey,
            this.priceType,
            this.price,
            this.quantity,
            this.lastQuantity,
            this.orderStatus,
            this.matched,
            this.createdAt,
            this.icon,
            this.fee,
            this.amounts,
            this.avgPrice,
            this.lastModifiedAt,
            this.version});
            resources.ApplyResources(this.dataGridViewUserOrder, "dataGridViewUserOrder");
            this.dataGridViewUserOrder.Name = "dataGridViewUserOrder";
            this.dataGridViewUserOrder.RowTemplate.Height = 23;
            // 
            // orderRevert
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = "撤单";
            this.orderRevert.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.orderRevert, "orderRevert");
            this.orderRevert.Name = "orderRevert";
            this.orderRevert.Text = "撤单";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            resources.ApplyResources(this.id, "id");
            this.id.Name = "id";
            // 
            // orderId
            // 
            this.orderId.DataPropertyName = "orderId";
            resources.ApplyResources(this.orderId, "orderId");
            this.orderId.Name = "orderId";
            // 
            // orderKey
            // 
            this.orderKey.DataPropertyName = "orderKey";
            resources.ApplyResources(this.orderKey, "orderKey");
            this.orderKey.Name = "orderKey";
            // 
            // memberId
            // 
            this.memberId.DataPropertyName = "memberId";
            resources.ApplyResources(this.memberId, "memberId");
            this.memberId.Name = "memberId";
            // 
            // memberKey
            // 
            this.memberKey.DataPropertyName = "memberKey";
            resources.ApplyResources(this.memberKey, "memberKey");
            this.memberKey.Name = "memberKey";
            // 
            // usr
            // 
            this.usr.DataPropertyName = "usr";
            resources.ApplyResources(this.usr, "usr");
            this.usr.Name = "usr";
            // 
            // clientId
            // 
            this.clientId.DataPropertyName = "clientId";
            resources.ApplyResources(this.clientId, "clientId");
            this.clientId.Name = "clientId";
            // 
            // orderType
            // 
            this.orderType.DataPropertyName = "orderType";
            resources.ApplyResources(this.orderType, "orderType");
            this.orderType.Name = "orderType";
            // 
            // pairCode
            // 
            this.pairCode.DataPropertyName = "pairCode";
            resources.ApplyResources(this.pairCode, "pairCode");
            this.pairCode.Name = "pairCode";
            // 
            // tradeCoinKey
            // 
            this.tradeCoinKey.DataPropertyName = "tradeCoinKey";
            resources.ApplyResources(this.tradeCoinKey, "tradeCoinKey");
            this.tradeCoinKey.Name = "tradeCoinKey";
            // 
            // priceCoinKey
            // 
            this.priceCoinKey.DataPropertyName = "priceCoinKey";
            resources.ApplyResources(this.priceCoinKey, "priceCoinKey");
            this.priceCoinKey.Name = "priceCoinKey";
            // 
            // priceType
            // 
            this.priceType.DataPropertyName = "priceType";
            resources.ApplyResources(this.priceType, "priceType");
            this.priceType.Name = "priceType";
            // 
            // price
            // 
            this.price.DataPropertyName = "price";
            resources.ApplyResources(this.price, "price");
            this.price.Name = "price";
            // 
            // quantity
            // 
            this.quantity.DataPropertyName = "quantity";
            resources.ApplyResources(this.quantity, "quantity");
            this.quantity.Name = "quantity";
            // 
            // lastQuantity
            // 
            this.lastQuantity.DataPropertyName = "lastQuantity";
            resources.ApplyResources(this.lastQuantity, "lastQuantity");
            this.lastQuantity.Name = "lastQuantity";
            // 
            // orderStatus
            // 
            this.orderStatus.DataPropertyName = "orderStatus";
            resources.ApplyResources(this.orderStatus, "orderStatus");
            this.orderStatus.Name = "orderStatus";
            // 
            // matched
            // 
            this.matched.DataPropertyName = "matched";
            resources.ApplyResources(this.matched, "matched");
            this.matched.Name = "matched";
            // 
            // createdAt
            // 
            this.createdAt.DataPropertyName = "createdAt";
            resources.ApplyResources(this.createdAt, "createdAt");
            this.createdAt.Name = "createdAt";
            // 
            // icon
            // 
            this.icon.DataPropertyName = "icon";
            resources.ApplyResources(this.icon, "icon");
            this.icon.Name = "icon";
            // 
            // fee
            // 
            this.fee.DataPropertyName = "fee";
            resources.ApplyResources(this.fee, "fee");
            this.fee.Name = "fee";
            // 
            // amounts
            // 
            this.amounts.DataPropertyName = "amounts";
            resources.ApplyResources(this.amounts, "amounts");
            this.amounts.Name = "amounts";
            // 
            // avgPrice
            // 
            this.avgPrice.DataPropertyName = "avgPrice";
            resources.ApplyResources(this.avgPrice, "avgPrice");
            this.avgPrice.Name = "avgPrice";
            // 
            // lastModifiedAt
            // 
            this.lastModifiedAt.DataPropertyName = "lastModifiedAt";
            resources.ApplyResources(this.lastModifiedAt, "lastModifiedAt");
            this.lastModifiedAt.Name = "lastModifiedAt";
            // 
            // version
            // 
            this.version.DataPropertyName = "version";
            resources.ApplyResources(this.version, "version");
            this.version.Name = "version";
            // 
            // groupBoxAdvancedSettings
            // 
            this.groupBoxAdvancedSettings.Controls.Add(this.SynchronizeAdvancedSettings);
            this.groupBoxAdvancedSettings.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.groupBoxAdvancedSettings, "groupBoxAdvancedSettings");
            this.groupBoxAdvancedSettings.Name = "groupBoxAdvancedSettings";
            this.groupBoxAdvancedSettings.TabStop = false;
            // 
            // SynchronizeAdvancedSettings
            // 
            resources.ApplyResources(this.SynchronizeAdvancedSettings, "SynchronizeAdvancedSettings");
            this.SynchronizeAdvancedSettings.Name = "SynchronizeAdvancedSettings";
            this.SynchronizeAdvancedSettings.TabStop = true;
            this.SynchronizeAdvancedSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SynchronizeAdvancedSettings_LinkClicked);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.checkBoxOfflinePrompt, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePickerLateHighPriceStart, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxTwoHigherPrices, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxEarlyHighPrice, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxLateHighPrice, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePickerTwoHigherPriceStart, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePickerEarlyHighPriceStart, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePickerLateHighPriceEnd, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePickerEarlyHighPriceEnd, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePickerTwoHigherPriceEnd, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxDealPrompt, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 4, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // checkBoxOfflinePrompt
            // 
            resources.ApplyResources(this.checkBoxOfflinePrompt, "checkBoxOfflinePrompt");
            this.checkBoxOfflinePrompt.Name = "checkBoxOfflinePrompt";
            this.checkBoxOfflinePrompt.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // dateTimePickerLateHighPriceStart
            // 
            resources.ApplyResources(this.dateTimePickerLateHighPriceStart, "dateTimePickerLateHighPriceStart");
            this.dateTimePickerLateHighPriceStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerLateHighPriceStart.Name = "dateTimePickerLateHighPriceStart";
            this.dateTimePickerLateHighPriceStart.ShowUpDown = true;
            this.dateTimePickerLateHighPriceStart.Value = new System.DateTime(2020, 5, 28, 19, 0, 0, 0);
            // 
            // checkBoxTwoHigherPrices
            // 
            resources.ApplyResources(this.checkBoxTwoHigherPrices, "checkBoxTwoHigherPrices");
            this.checkBoxTwoHigherPrices.Name = "checkBoxTwoHigherPrices";
            this.checkBoxTwoHigherPrices.UseVisualStyleBackColor = true;
            // 
            // checkBoxEarlyHighPrice
            // 
            resources.ApplyResources(this.checkBoxEarlyHighPrice, "checkBoxEarlyHighPrice");
            this.checkBoxEarlyHighPrice.Name = "checkBoxEarlyHighPrice";
            this.toolTip1.SetToolTip(this.checkBoxEarlyHighPrice, resources.GetString("checkBoxEarlyHighPrice.ToolTip"));
            this.checkBoxEarlyHighPrice.UseVisualStyleBackColor = true;
            // 
            // checkBoxLateHighPrice
            // 
            resources.ApplyResources(this.checkBoxLateHighPrice, "checkBoxLateHighPrice");
            this.checkBoxLateHighPrice.Name = "checkBoxLateHighPrice";
            this.checkBoxLateHighPrice.Tag = "";
            this.checkBoxLateHighPrice.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerTwoHigherPriceStart
            // 
            resources.ApplyResources(this.dateTimePickerTwoHigherPriceStart, "dateTimePickerTwoHigherPriceStart");
            this.dateTimePickerTwoHigherPriceStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTwoHigherPriceStart.Name = "dateTimePickerTwoHigherPriceStart";
            this.dateTimePickerTwoHigherPriceStart.ShowUpDown = true;
            this.dateTimePickerTwoHigherPriceStart.Value = new System.DateTime(2020, 5, 28, 18, 0, 0, 0);
            // 
            // dateTimePickerEarlyHighPriceStart
            // 
            resources.ApplyResources(this.dateTimePickerEarlyHighPriceStart, "dateTimePickerEarlyHighPriceStart");
            this.dateTimePickerEarlyHighPriceStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEarlyHighPriceStart.Name = "dateTimePickerEarlyHighPriceStart";
            this.dateTimePickerEarlyHighPriceStart.ShowUpDown = true;
            this.dateTimePickerEarlyHighPriceStart.Value = new System.DateTime(2020, 5, 28, 7, 0, 0, 0);
            // 
            // dateTimePickerLateHighPriceEnd
            // 
            resources.ApplyResources(this.dateTimePickerLateHighPriceEnd, "dateTimePickerLateHighPriceEnd");
            this.dateTimePickerLateHighPriceEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerLateHighPriceEnd.Name = "dateTimePickerLateHighPriceEnd";
            this.dateTimePickerLateHighPriceEnd.ShowUpDown = true;
            this.dateTimePickerLateHighPriceEnd.Value = new System.DateTime(2020, 5, 28, 20, 59, 0, 0);
            // 
            // dateTimePickerEarlyHighPriceEnd
            // 
            resources.ApplyResources(this.dateTimePickerEarlyHighPriceEnd, "dateTimePickerEarlyHighPriceEnd");
            this.dateTimePickerEarlyHighPriceEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEarlyHighPriceEnd.Name = "dateTimePickerEarlyHighPriceEnd";
            this.dateTimePickerEarlyHighPriceEnd.ShowUpDown = true;
            this.dateTimePickerEarlyHighPriceEnd.Value = new System.DateTime(2020, 5, 28, 8, 59, 0, 0);
            // 
            // dateTimePickerTwoHigherPriceEnd
            // 
            resources.ApplyResources(this.dateTimePickerTwoHigherPriceEnd, "dateTimePickerTwoHigherPriceEnd");
            this.dateTimePickerTwoHigherPriceEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTwoHigherPriceEnd.Name = "dateTimePickerTwoHigherPriceEnd";
            this.dateTimePickerTwoHigherPriceEnd.ShowUpDown = true;
            this.dateTimePickerTwoHigherPriceEnd.Value = new System.DateTime(2020, 5, 28, 8, 59, 0, 0);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBoxDealPrompt
            // 
            resources.ApplyResources(this.checkBoxDealPrompt, "checkBoxDealPrompt");
            this.checkBoxDealPrompt.Name = "checkBoxDealPrompt";
            this.checkBoxDealPrompt.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numericUpDownRetainedAmount);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // numericUpDownRetainedAmount
            // 
            resources.ApplyResources(this.numericUpDownRetainedAmount, "numericUpDownRetainedAmount");
            this.numericUpDownRetainedAmount.DecimalPlaces = 2;
            this.numericUpDownRetainedAmount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownRetainedAmount.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.numericUpDownRetainedAmount.Name = "numericUpDownRetainedAmount";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.panelAction);
            resources.ApplyResources(this.groupBoxAction, "groupBoxAction");
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.TabStop = false;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonMine);
            this.panelAction.Controls.Add(this.label_NGRC_value);
            this.panelAction.Controls.Add(this.label14);
            this.panelAction.Controls.Add(this.label_USDT_value);
            this.panelAction.Controls.Add(this.label12);
            this.panelAction.Controls.Add(this.labelCountTradeToday);
            this.panelAction.Controls.Add(this.label10);
            this.panelAction.Controls.Add(this.labelTodayRound);
            this.panelAction.Controls.Add(this.labelCurrentPrice);
            this.panelAction.Controls.Add(this.label6);
            this.panelAction.Controls.Add(this.label5);
            this.panelAction.Controls.Add(this.label4);
            this.panelAction.Controls.Add(this.label3);
            this.panelAction.Controls.Add(this.label2);
            this.panelAction.Controls.Add(this.labelUserName);
            resources.ApplyResources(this.panelAction, "panelAction");
            this.panelAction.Name = "panelAction";
            // 
            // buttonMine
            // 
            resources.ApplyResources(this.buttonMine, "buttonMine");
            this.buttonMine.BackColor = System.Drawing.Color.Pink;
            this.buttonMine.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonMine.Name = "buttonMine";
            this.buttonMine.UseVisualStyleBackColor = false;
            this.buttonMine.MouseLeave += new System.EventHandler(this.buttonMine_MouseLeave);
            this.buttonMine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonMine_MouseMove);
            // 
            // label_NGRC_value
            // 
            resources.ApplyResources(this.label_NGRC_value, "label_NGRC_value");
            this.label_NGRC_value.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_NGRC_value.Name = "label_NGRC_value";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label_USDT_value
            // 
            resources.ApplyResources(this.label_USDT_value, "label_USDT_value");
            this.label_USDT_value.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_USDT_value.Name = "label_USDT_value";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // labelCountTradeToday
            // 
            resources.ApplyResources(this.labelCountTradeToday, "labelCountTradeToday");
            this.labelCountTradeToday.ForeColor = System.Drawing.Color.Red;
            this.labelCountTradeToday.Name = "labelCountTradeToday";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // labelTodayRound
            // 
            resources.ApplyResources(this.labelTodayRound, "labelTodayRound");
            this.labelTodayRound.ForeColor = System.Drawing.Color.Red;
            this.labelTodayRound.Name = "labelTodayRound";
            // 
            // labelCurrentPrice
            // 
            resources.ApplyResources(this.labelCurrentPrice, "labelCurrentPrice");
            this.labelCurrentPrice.ForeColor = System.Drawing.Color.Red;
            this.labelCurrentPrice.Name = "labelCurrentPrice";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelUserName
            // 
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.Name = "labelUserName";
            // 
            // timerChart
            // 
            this.timerChart.Enabled = true;
            this.timerChart.Interval = 500;
            // 
            // timerAction
            // 
            this.timerAction.Enabled = true;
            this.timerAction.Interval = 60000;
            this.timerAction.Tick += new System.EventHandler(this.timerAction_Tick);
            // 
            // timerInit
            // 
            this.timerInit.Interval = 60000;
            this.timerInit.Tick += new System.EventHandler(this.timerInit_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMineToolStripMenuItem,
            this.设置ToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuMineToolStripMenuItem
            // 
            this.menuMineToolStripMenuItem.Image = global::ForWinQuant.Properties.Resources.用户;
            this.menuMineToolStripMenuItem.Name = "menuMineToolStripMenuItem";
            resources.ApplyResources(this.menuMineToolStripMenuItem, "menuMineToolStripMenuItem");
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SynchronizeAdvancedSettingsMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            resources.ApplyResources(this.设置ToolStripMenuItem, "设置ToolStripMenuItem");
            // 
            // SynchronizeAdvancedSettingsMenuItem
            // 
            this.SynchronizeAdvancedSettingsMenuItem.Name = "SynchronizeAdvancedSettingsMenuItem";
            resources.ApplyResources(this.SynchronizeAdvancedSettingsMenuItem, "SynchronizeAdvancedSettingsMenuItem");
            // 
            // BottomToolStripPanel
            // 
            resources.ApplyResources(this.BottomToolStripPanel, "BottomToolStripPanel");
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // TopToolStripPanel
            // 
            resources.ApplyResources(this.TopToolStripPanel, "TopToolStripPanel");
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // RightToolStripPanel
            // 
            resources.ApplyResources(this.RightToolStripPanel, "RightToolStripPanel");
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // LeftToolStripPanel
            // 
            resources.ApplyResources(this.LeftToolStripPanel, "LeftToolStripPanel");
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // ContentPanel
            // 
            resources.ApplyResources(this.ContentPanel, "ContentPanel");
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerMine.Panel1.ResumeLayout(false);
            this.splitContainerMine.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMine)).EndInit();
            this.splitContainerMine.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxExecutionLog.ResumeLayout(false);
            this.groupBoxDelegateList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserOrder)).EndInit();
            this.groupBoxAdvancedSettings.ResumeLayout(false);
            this.groupBoxAdvancedSettings.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRetainedAmount)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.panelAction.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerChart;
        private System.Windows.Forms.Timer timerAction;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuMineToolStripMenuItem;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLogin;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarRequest;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainerMine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Label label_NGRC_value;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_USDT_value;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelCountTradeToday;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelTodayRound;
        private System.Windows.Forms.Label labelCurrentPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.GroupBox groupBoxExecutionLog;
        private System.Windows.Forms.GroupBox groupBoxDelegateList;
        private System.Windows.Forms.GroupBox groupBoxAdvancedSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxOfflinePrompt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePickerLateHighPriceStart;
        private System.Windows.Forms.CheckBox checkBoxTwoHigherPrices;
        private System.Windows.Forms.CheckBox checkBoxEarlyHighPrice;
        private System.Windows.Forms.CheckBox checkBoxLateHighPrice;
        private System.Windows.Forms.DateTimePicker dateTimePickerTwoHigherPriceStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEarlyHighPriceStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerLateHighPriceEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerEarlyHighPriceEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerTwoHigherPriceEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxDealPrompt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAssetUpdateTime;
        private System.Windows.Forms.NumericUpDown numericUpDownRetainedAmount;
        public System.Windows.Forms.TreeView treeViewSubUsers;
        private System.Windows.Forms.RichTextBox textBoxLog;
        public System.Windows.Forms.DataGridView dataGridViewUserOrder;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        public System.Windows.Forms.Button buttonMine;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SynchronizeAdvancedSettingsMenuItem;
        private System.Windows.Forms.LinkLabel SynchronizeAdvancedSettings;
        private System.Windows.Forms.DataGridViewButtonColumn orderRevert;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberId;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn usr;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientId;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn pairCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn tradeCoinKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceCoinKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn matched;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn icon;
        private System.Windows.Forms.DataGridViewTextBoxColumn fee;
        private System.Windows.Forms.DataGridViewTextBoxColumn amounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastModifiedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn version;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

