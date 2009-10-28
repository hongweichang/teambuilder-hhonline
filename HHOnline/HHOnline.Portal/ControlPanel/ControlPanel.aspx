<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="ControlPanel.aspx.cs" Inherits="ControlPanel_ControlPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
<div style="width:100%;float:left;margin:10px auto;">
    <div class="fieldset_m" style="width:49%">
        <h4>待审核企业信息</h4>
        <div class="clPending">
           <hc:CompanyList ID="clCompany" runat="server" />
        </div>
    </div>
     <div class="fieldset_m" style="width:49%">
        <h4 class="personh4">待审核客户类型变更</h4>
        <div class="clPending">
            <hc:PendingList ID="plCompany" runat="server" />
        </div>
    </div>
</div>
<div style="width:100%;float:left;margin-top:30px;">
    <div class="fieldset_m" style="width:98%">
        <h4 class="pendingh4">待审核供应商录入产品</h4>
        <div class="clPending">
            <hc:SupplyList ID="slProduct" runat="server" />
        </div>
    </div>
</div>
</asp:Content>

