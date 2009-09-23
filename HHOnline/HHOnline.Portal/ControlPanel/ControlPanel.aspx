<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPanel/Masters/ControlPanelMaster.master" AutoEventWireup="true" CodeFile="ControlPanel.aspx.cs" Inherits="ControlPanel_ControlPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="fieldset_m">
        <h4>待审核企业信息</h4>
        <div class="clPending">
           <hc:CompanyList ID="clCompany" runat="server" />
        </div>
    </div>
</asp:Content>

