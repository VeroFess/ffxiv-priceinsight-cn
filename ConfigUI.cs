using System;
using ImGuiNET;

namespace PriceInsight;

class ConfigUI : IDisposable {
    private readonly PriceInsightPlugin plugin;

    private bool settingsVisible = false;

    public bool SettingsVisible {
        get => settingsVisible;
        set => settingsVisible = value;
    }

    public ConfigUI(PriceInsightPlugin plugin)
    {
        this.plugin = plugin;
    }

    public void Dispose()
    {
    }

    public void Draw()
    {
        if (!SettingsVisible) {
            return;
        }

        var conf = plugin.Configuration;
        if (ImGui.Begin("Price Insight 设置", ref settingsVisible,
                ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize)) {
            var configValue = conf.RefreshWithAlt;
            if (ImGui.Checkbox("点击 Alt 刷新价格", ref configValue)) {
                conf.RefreshWithAlt = configValue;
                conf.Save();
            }

            configValue = conf.PrefetchInventory;
            if (ImGui.Checkbox("预取库存项目的价格", ref configValue)) {
                conf.PrefetchInventory = configValue;
                conf.Save();
            }
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("登录时预取背包、陆行鸟鞍袋和雇员中所有物品的价格.\n警告: 启用 \"中国区\" 设置会导致高网络负载.");

            configValue = conf.UseCurrentWorld;
            if (ImGui.Checkbox("将当前所在的服务器视为原始服务器", ref configValue)) {
                conf.UseCurrentWorld = configValue;
                conf.Save();
                plugin.ClearCache();
            }
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("您所在的当前服务器将被视为您的\"原始服务器\".\n如果您正在跨服旅行并希望查看那里的价格，这很有用");
            ImGui.Separator();
            ImGui.PushID(0);

            ImGui.Text("显示以下范围内最便宜的价格:");

            configValue = conf.ShowRegion;
            if (ImGui.Checkbox("中国区", ref configValue)) {
                conf.ShowRegion = configValue;
                conf.Save();
                plugin.ClearCache();
            }
            TooltipRegion();

            configValue = conf.ShowDatacenter;
            if (ImGui.Checkbox("大区", ref configValue)) {
                conf.ShowDatacenter = configValue;
                conf.Save();
                plugin.ClearCache();
            }

            configValue = conf.ShowWorld;
            if (ImGui.Checkbox("原始服务器", ref configValue)) {
                conf.ShowWorld = configValue;
                conf.Save();
            }

            ImGui.PopID();
            ImGui.Separator();
            ImGui.PushID(1);

            ImGui.Text("显示以下范围的数据:");

            configValue = conf.ShowMostRecentPurchaseRegion;
            if (ImGui.Checkbox("中国区", ref configValue)) {
                conf.ShowMostRecentPurchaseRegion = configValue;
                conf.Save();
                plugin.ClearCache();
            }
            TooltipRegion();

            configValue = conf.ShowMostRecentPurchase;
            if (ImGui.Checkbox("大区", ref configValue)) {
                conf.ShowMostRecentPurchase = configValue;
                conf.Save();
                plugin.ClearCache();
            }

            configValue = conf.ShowMostRecentPurchaseWorld;
            if (ImGui.Checkbox("原始服务器", ref configValue)) {
                conf.ShowMostRecentPurchaseWorld = configValue;
                conf.Save();
            }

            ImGui.PopID();
            ImGui.Separator();

            configValue = conf.ShowDailySaleVelocity;
            if (ImGui.Checkbox("显示每天的销售额", ref configValue)) {
                conf.ShowDailySaleVelocity = configValue;
                conf.Save();
            }
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("根据最近 20 次购买显示每天的平均销售额。");

            configValue = conf.ShowAverageSalePrice;
            if (ImGui.Checkbox("显示平均售价", ref configValue)) {
                conf.ShowAverageSalePrice = configValue;
                conf.Save();
            }
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("显示基于最近 20 次购买的平均销售价格。");
            
            configValue = conf.ShowStackSalePrice;
            if (ImGui.Checkbox("显示合计价格", ref configValue)) {
                conf.ShowStackSalePrice = configValue;
                conf.Save();
            }
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("显示悬停物品如果以给定的单价出售的情况下合计的价格(单价 x 数量)。");
        }

        ImGui.End();
    }

    private static void TooltipRegion()
    {
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("显示整个国区的数据... 虽然我不知道有啥用但是还是放在这里");
    }
}