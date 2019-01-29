<%@ Assembly Name="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page MasterPageFile="~masterurl/default.master"
    Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage" %>

<%@ Register TagPrefix="WebPartPages"
    Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>


<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <WebPartPages:WebPartZone ID="Main" Title="Main Web Part Zone"
        FrameType="None" runat="server">
    </WebPartPages:WebPartZone>

</asp:Content>


<asp:Content ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Data Grid
</asp:Content>


<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Data Grid
</asp:Content>
