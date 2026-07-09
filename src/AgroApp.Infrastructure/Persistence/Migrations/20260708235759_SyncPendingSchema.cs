using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncPendingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertRules_plots_PlotId",
                table: "AlertRules");

            migrationBuilder.DropForeignKey(
                name: "FK_AlertRules_tenants_TenantId",
                table: "AlertRules");

            migrationBuilder.DropForeignKey(
                name: "FK_alerts_SensorDevices_DeviceId",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_alerts_plots_PlotId",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_alerts_tenants_TenantId",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_CostEntries_crops_CropId",
                table: "CostEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_CropImages_crops_CropId",
                table: "CropImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CropImages_users_UserId",
                table: "CropImages");

            migrationBuilder.DropForeignKey(
                name: "FK_crops_plots_PlotId",
                table: "crops");

            migrationBuilder.DropForeignKey(
                name: "FK_farms_tenants_TenantId",
                table: "farms");

            migrationBuilder.DropForeignKey(
                name: "FK_farms_users_OwnerId",
                table: "farms");

            migrationBuilder.DropForeignKey(
                name: "FK_fertilization_logs_crops_CropId",
                table: "fertilization_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_fertilization_logs_users_UserId",
                table: "fertilization_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_irrigation_logs_crops_CropId",
                table: "irrigation_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_irrigation_logs_users_UserId",
                table: "irrigation_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_labor_logs_crops_CropId",
                table: "labor_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_labor_logs_users_UserId",
                table: "labor_logs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhenologyStages_crops_CropId",
                table: "PhenologyStages");

            migrationBuilder.DropForeignKey(
                name: "FK_plots_farms_FarmId",
                table: "plots");

            migrationBuilder.DropForeignKey(
                name: "FK_sensor_readings_SensorDevices_DeviceId",
                table: "sensor_readings");

            migrationBuilder.DropForeignKey(
                name: "FK_SensorDevices_plots_PlotId",
                table: "SensorDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_users_tenants_TenantId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tenants",
                table: "tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sensor_readings",
                table: "sensor_readings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plots",
                table: "plots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_labor_logs",
                table: "labor_logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_irrigation_logs",
                table: "irrigation_logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fertilization_logs",
                table: "fertilization_logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_farms",
                table: "farms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_crops",
                table: "crops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_alerts",
                table: "alerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SensorDevices",
                table: "SensorDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhenologyStages",
                table: "PhenologyStages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CropImages",
                table: "CropImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostEntries",
                table: "CostEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlertRules",
                table: "AlertRules");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "labor_logs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "irrigation_logs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "fertilization_logs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "alerts");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "alerts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CropImages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CostEntries");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AlertRules");

            migrationBuilder.RenameTable(
                name: "SensorDevices",
                newName: "sensor_devices");

            migrationBuilder.RenameTable(
                name: "PhenologyStages",
                newName: "phenology_stages");

            migrationBuilder.RenameTable(
                name: "CropImages",
                newName: "crop_images");

            migrationBuilder.RenameTable(
                name: "CostEntries",
                newName: "cost_entries");

            migrationBuilder.RenameTable(
                name: "AlertRules",
                newName: "alert_rules");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "users",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LastLoginAt",
                table: "users",
                newName: "last_login_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "users",
                newName: "is_active");

            migrationBuilder.RenameIndex(
                name: "IX_users_TenantId_Email",
                table: "users",
                newName: "ix_users_tenant_id_email");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "tenants",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "Plan",
                table: "tenants",
                newName: "plan");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tenants",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tenants",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tenants",
                newName: "is_active");

            migrationBuilder.RenameIndex(
                name: "IX_tenants_Slug",
                table: "tenants",
                newName: "ix_tenants_slug");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "sensor_readings",
                newName: "temperature");

            migrationBuilder.RenameColumn(
                name: "Ph",
                table: "sensor_readings",
                newName: "ph");

            migrationBuilder.RenameColumn(
                name: "Luminosity",
                table: "sensor_readings",
                newName: "luminosity");

            migrationBuilder.RenameColumn(
                name: "Ec",
                table: "sensor_readings",
                newName: "ec");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sensor_readings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RainMm",
                table: "sensor_readings",
                newName: "rain_mm");

            migrationBuilder.RenameColumn(
                name: "HumiditySoil",
                table: "sensor_readings",
                newName: "humidity_soil");

            migrationBuilder.RenameColumn(
                name: "HumidityAir",
                table: "sensor_readings",
                newName: "humidity_air");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "sensor_readings",
                newName: "device_id");

            migrationBuilder.RenameIndex(
                name: "IX_sensor_readings_DeviceId",
                table: "sensor_readings",
                newName: "ix_sensor_readings_device_id");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "plots",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "plots",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "GeoJson",
                table: "plots",
                newName: "geojson");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "plots",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SoilType",
                table: "plots",
                newName: "soil_type");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "plots",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "plots",
                newName: "farm_id");

            migrationBuilder.RenameColumn(
                name: "AreaHa",
                table: "plots",
                newName: "area_ha");

            migrationBuilder.RenameIndex(
                name: "IX_plots_FarmId",
                table: "plots",
                newName: "ix_plots_farm_id");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "labor_logs",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "labor_logs",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "labor_logs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WorkersCount",
                table: "labor_logs",
                newName: "workers_count");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "labor_logs",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PerformedAt",
                table: "labor_logs",
                newName: "performed_at");

            migrationBuilder.RenameColumn(
                name: "HoursWorked",
                table: "labor_logs",
                newName: "hours_worked");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "labor_logs",
                newName: "crop_id");

            migrationBuilder.RenameColumn(
                name: "ActivityType",
                table: "labor_logs",
                newName: "activity_type");

            migrationBuilder.RenameIndex(
                name: "IX_labor_logs_UserId",
                table: "labor_logs",
                newName: "ix_labor_logs_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_labor_logs_CropId",
                table: "labor_logs",
                newName: "ix_labor_logs_crop_id");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "irrigation_logs",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Method",
                table: "irrigation_logs",
                newName: "method");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "irrigation_logs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VolumeLiters",
                table: "irrigation_logs",
                newName: "volume_liters");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "irrigation_logs",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "DurationMin",
                table: "irrigation_logs",
                newName: "duration_min");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "irrigation_logs",
                newName: "crop_id");

            migrationBuilder.RenameColumn(
                name: "AppliedAt",
                table: "irrigation_logs",
                newName: "applied_at");

            migrationBuilder.RenameIndex(
                name: "IX_irrigation_logs_UserId",
                table: "irrigation_logs",
                newName: "ix_irrigation_logs_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_irrigation_logs_CropId",
                table: "irrigation_logs",
                newName: "ix_irrigation_logs_crop_id");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "fertilization_logs",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Method",
                table: "fertilization_logs",
                newName: "method");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "fertilization_logs",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "fertilization_logs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "fertilization_logs",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TotalKg",
                table: "fertilization_logs",
                newName: "total_kg");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "fertilization_logs",
                newName: "product_type");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "fertilization_logs",
                newName: "product_name");

            migrationBuilder.RenameColumn(
                name: "NextApplication",
                table: "fertilization_logs",
                newName: "next_application");

            migrationBuilder.RenameColumn(
                name: "DoseKgHa",
                table: "fertilization_logs",
                newName: "dose_kg_ha");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "fertilization_logs",
                newName: "crop_id");

            migrationBuilder.RenameColumn(
                name: "AppliedAt",
                table: "fertilization_logs",
                newName: "applied_at");

            migrationBuilder.RenameIndex(
                name: "IX_fertilization_logs_UserId",
                table: "fertilization_logs",
                newName: "ix_fertilization_logs_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_fertilization_logs_CropId",
                table: "fertilization_logs",
                newName: "ix_fertilization_logs_crop_id");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "farms",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "farms",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "farms",
                newName: "lng");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "farms",
                newName: "lat");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "farms",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "farms",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "farms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "farms",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "farms",
                newName: "owner_id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "farms",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "AreaHa",
                table: "farms",
                newName: "area_ha");

            migrationBuilder.RenameIndex(
                name: "IX_farms_TenantId",
                table: "farms",
                newName: "ix_farms_tenant_id");

            migrationBuilder.RenameIndex(
                name: "IX_farms_OwnerId",
                table: "farms",
                newName: "ix_farms_owner_id");

            migrationBuilder.RenameColumn(
                name: "Variety",
                table: "crops",
                newName: "variety");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "crops",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "crops",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "crops",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "YieldKg",
                table: "crops",
                newName: "yield_kg");

            migrationBuilder.RenameColumn(
                name: "PlotId",
                table: "crops",
                newName: "plot_id");

            migrationBuilder.RenameColumn(
                name: "PlantedAt",
                table: "crops",
                newName: "planted_at");

            migrationBuilder.RenameColumn(
                name: "HarvestedAt",
                table: "crops",
                newName: "harvested_at");

            migrationBuilder.RenameColumn(
                name: "EstimatedHarvest",
                table: "crops",
                newName: "estimated_harvest");

            migrationBuilder.RenameColumn(
                name: "CropType",
                table: "crops",
                newName: "crop_type");

            migrationBuilder.RenameIndex(
                name: "IX_crops_PlotId",
                table: "crops",
                newName: "ix_crops_plot_id");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "alerts",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "alerts",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "alerts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TriggeredAt",
                table: "alerts",
                newName: "triggered_at");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "alerts",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "ReadAt",
                table: "alerts",
                newName: "read_at");

            migrationBuilder.RenameColumn(
                name: "PlotId",
                table: "alerts",
                newName: "plot_id");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "alerts",
                newName: "is_read");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "alerts",
                newName: "device_id");

            migrationBuilder.RenameColumn(
                name: "AlertType",
                table: "alerts",
                newName: "alert_type");

            migrationBuilder.RenameIndex(
                name: "IX_alerts_TenantId",
                table: "alerts",
                newName: "ix_alerts_tenant_id");

            migrationBuilder.RenameIndex(
                name: "IX_alerts_PlotId",
                table: "alerts",
                newName: "ix_alerts_plot_id");

            migrationBuilder.RenameIndex(
                name: "IX_alerts_DeviceId",
                table: "alerts",
                newName: "ix_alerts_device_id");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "sensor_devices",
                newName: "lng");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "sensor_devices",
                newName: "lat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sensor_devices",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "sensor_devices",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PlotId",
                table: "sensor_devices",
                newName: "plot_id");

            migrationBuilder.RenameColumn(
                name: "LastSeenAt",
                table: "sensor_devices",
                newName: "last_seen_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "sensor_devices",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "FirmwareVer",
                table: "sensor_devices",
                newName: "firmware_ver");

            migrationBuilder.RenameColumn(
                name: "DeviceType",
                table: "sensor_devices",
                newName: "device_type");

            migrationBuilder.RenameColumn(
                name: "DeviceCode",
                table: "sensor_devices",
                newName: "device_code");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "sensor_devices",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BatteryPct",
                table: "sensor_devices",
                newName: "battery_pct");

            migrationBuilder.RenameIndex(
                name: "IX_SensorDevices_PlotId",
                table: "sensor_devices",
                newName: "ix_sensor_devices_plot_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "phenology_stages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "phenology_stages",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "phenology_stages",
                newName: "started_at");

            migrationBuilder.RenameColumn(
                name: "StageName",
                table: "phenology_stages",
                newName: "stage_name");

            migrationBuilder.RenameColumn(
                name: "EndedAt",
                table: "phenology_stages",
                newName: "ended_at");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "phenology_stages",
                newName: "crop_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "phenology_stages",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "phenology_stages",
                newName: "observations");

            migrationBuilder.RenameIndex(
                name: "IX_PhenologyStages_CropId",
                table: "phenology_stages",
                newName: "ix_phenology_stages_crop_id");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "crop_images",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "crop_images",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "crop_images",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "crop_images",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TakenAt",
                table: "crop_images",
                newName: "taken_at");

            migrationBuilder.RenameColumn(
                name: "StorageKey",
                table: "crop_images",
                newName: "storage_key");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "crop_images",
                newName: "crop_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "crop_images",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AiDiagnosis",
                table: "crop_images",
                newName: "ai_diagnosis");

            migrationBuilder.RenameColumn(
                name: "AiConfidence",
                table: "crop_images",
                newName: "ai_confidence");

            migrationBuilder.RenameIndex(
                name: "IX_CropImages_UserId",
                table: "crop_images",
                newName: "ix_crop_images_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_CropImages_CropId",
                table: "crop_images",
                newName: "ix_crop_images_crop_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "cost_entries",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "cost_entries",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "cost_entries",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cost_entries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "cost_entries",
                newName: "entry_date");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "cost_entries",
                newName: "crop_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "cost_entries",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_CostEntries_CropId",
                table: "cost_entries",
                newName: "ix_cost_entries_crop_id");

            migrationBuilder.RenameColumn(
                name: "Threshold",
                table: "alert_rules",
                newName: "threshold");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "alert_rules",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "Operator",
                table: "alert_rules",
                newName: "operator");

            migrationBuilder.RenameColumn(
                name: "Metric",
                table: "alert_rules",
                newName: "metric");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "alert_rules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "alert_rules",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "PlotId",
                table: "alert_rules",
                newName: "plot_id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "alert_rules",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "alert_rules",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_AlertRules_TenantId",
                table: "alert_rules",
                newName: "ix_alert_rules_tenant_id");

            migrationBuilder.RenameIndex(
                name: "IX_AlertRules_PlotId",
                table: "alert_rules",
                newName: "ix_alert_rules_plot_id");

            migrationBuilder.AddColumn<Guid>(
                name: "task_id",
                table: "labor_logs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "task_id",
                table: "irrigation_logs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "task_id",
                table: "fertilization_logs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_read",
                table: "alerts",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<Guid>(
                name: "crop_id",
                table: "alerts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "stage_name",
                table: "phenology_stages",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "is_custom",
                table: "phenology_stages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "stage_order",
                table: "phenology_stages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "template_id",
                table: "phenology_stages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                table: "phenology_stages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "crop_images",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "crop_images",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "storage_key",
                table: "crop_images",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<float>(
                name: "ai_confidence",
                table: "crop_images",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ai_analyzed_at",
                table: "crop_images",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tenants",
                table: "tenants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sensor_readings",
                table: "sensor_readings",
                columns: new[] { "id", "recorded_at" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_plots",
                table: "plots",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_labor_logs",
                table: "labor_logs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_irrigation_logs",
                table: "irrigation_logs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_fertilization_logs",
                table: "fertilization_logs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_farms",
                table: "farms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_crops",
                table: "crops",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_alerts",
                table: "alerts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sensor_devices",
                table: "sensor_devices",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_phenology_stages",
                table: "phenology_stages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_crop_images",
                table: "crop_images",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cost_entries",
                table: "cost_entries",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_alert_rules",
                table: "alert_rules",
                column: "id");

            migrationBuilder.CreateTable(
                name: "fcm_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    platform = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fcm_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_fcm_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phenology_templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    crop_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    stage_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    stage_order = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    min_days = table.Column<int>(type: "integer", nullable: false),
                    max_days = table.Column<int>(type: "integer", nullable: false),
                    icon = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    recommendations = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phenology_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_revoked = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    plot_id = table.Column<Guid>(type: "uuid", nullable: true),
                    crop_id = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    task_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    shift = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    recurrence_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    week_days = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_templates", x => x.id);
                    table.ForeignKey(
                        name: "fk_task_templates_crops_crop_id",
                        column: x => x.crop_id,
                        principalTable: "crops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_task_templates_plots_plot_id",
                        column: x => x.plot_id,
                        principalTable: "plots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_task_templates_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_task_templates_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    assigned_to = table.Column<Guid>(type: "uuid", nullable: false),
                    plot_id = table.Column<Guid>(type: "uuid", nullable: true),
                    crop_id = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    task_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_tasks_crops_crop_id",
                        column: x => x.crop_id,
                        principalTable: "crops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_tasks_plots_plot_id",
                        column: x => x.plot_id,
                        principalTable: "plots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_tasks_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tasks_users_assigned_to",
                        column: x => x.assigned_to,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tasks_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "task_occurrences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assigned_to = table.Column<Guid>(type: "uuid", nullable: true),
                    scheduled_date = table.Column<DateOnly>(type: "date", nullable: false),
                    shift = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_occurrences", x => x.id);
                    table.ForeignKey(
                        name: "fk_task_occurrences_task_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "task_templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_task_occurrences_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_task_occurrences_users_assigned_to",
                        column: x => x.assigned_to,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_labor_logs_task_id",
                table: "labor_logs",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_irrigation_logs_task_id",
                table: "irrigation_logs",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_fertilization_logs_task_id",
                table: "fertilization_logs",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_alerts_crop_id",
                table: "alerts",
                column: "crop_id");

            migrationBuilder.CreateIndex(
                name: "ix_phenology_stages_template_id",
                table: "phenology_stages",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_phenology_stages_tenant_id",
                table: "phenology_stages",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_fcm_tokens_user_id_token",
                table: "fcm_tokens",
                columns: new[] { "user_id", "token" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_occurrences_assigned_to",
                table: "task_occurrences",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "ix_task_occurrences_template_id",
                table: "task_occurrences",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_occurrences_tenant_id",
                table: "task_occurrences",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_templates_created_by",
                table: "task_templates",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_task_templates_crop_id",
                table: "task_templates",
                column: "crop_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_templates_plot_id",
                table: "task_templates",
                column: "plot_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_templates_tenant_id",
                table: "task_templates",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_assigned_to",
                table: "tasks",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_created_by",
                table: "tasks",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_crop_id",
                table: "tasks",
                column: "crop_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_plot_id",
                table: "tasks",
                column: "plot_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_tenant_id",
                table: "tasks",
                column: "tenant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_alert_rules_plots_plot_id",
                table: "alert_rules",
                column: "plot_id",
                principalTable: "plots",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_alert_rules_tenants_tenant_id",
                table: "alert_rules",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_alerts_crops_crop_id",
                table: "alerts",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_alerts_plots_plot_id",
                table: "alerts",
                column: "plot_id",
                principalTable: "plots",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_alerts_sensor_devices_device_id",
                table: "alerts",
                column: "device_id",
                principalTable: "sensor_devices",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_alerts_tenants_tenant_id",
                table: "alerts",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cost_entries_crops_crop_id",
                table: "cost_entries",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_crop_images_crops",
                table: "crop_images",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_crop_images_users",
                table: "crop_images",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_crops_plots_plot_id",
                table: "crops",
                column: "plot_id",
                principalTable: "plots",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_farms_tenants_tenant_id",
                table: "farms",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_farms_users_owner_id",
                table: "farms",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fertilization_logs_crops_crop_id",
                table: "fertilization_logs",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_fertilization_logs_tasks_task_id",
                table: "fertilization_logs",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_fertilization_logs_users_user_id",
                table: "fertilization_logs",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_irrigation_logs_crops_crop_id",
                table: "irrigation_logs",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_irrigation_logs_tasks_task_id",
                table: "irrigation_logs",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_irrigation_logs_users_user_id",
                table: "irrigation_logs",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_labor_logs_crops_crop_id",
                table: "labor_logs",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_labor_logs_tasks_task_id",
                table: "labor_logs",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_labor_logs_users_user_id",
                table: "labor_logs",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_phenology_stages_crops_crop_id",
                table: "phenology_stages",
                column: "crop_id",
                principalTable: "crops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_phenology_stages_phenology_templates_template_id",
                table: "phenology_stages",
                column: "template_id",
                principalTable: "phenology_templates",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_phenology_stages_tenants_tenant_id",
                table: "phenology_stages",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_plots_farms_farm_id",
                table: "plots",
                column: "farm_id",
                principalTable: "farms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sensor_devices_plots_plot_id",
                table: "sensor_devices",
                column: "plot_id",
                principalTable: "plots",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sensor_readings_sensor_devices_device_id",
                table: "sensor_readings",
                column: "device_id",
                principalTable: "sensor_devices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_tenants_tenant_id",
                table: "users",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_alert_rules_plots_plot_id",
                table: "alert_rules");

            migrationBuilder.DropForeignKey(
                name: "fk_alert_rules_tenants_tenant_id",
                table: "alert_rules");

            migrationBuilder.DropForeignKey(
                name: "fk_alerts_crops_crop_id",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "fk_alerts_plots_plot_id",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "fk_alerts_sensor_devices_device_id",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "fk_alerts_tenants_tenant_id",
                table: "alerts");

            migrationBuilder.DropForeignKey(
                name: "fk_cost_entries_crops_crop_id",
                table: "cost_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_crop_images_crops",
                table: "crop_images");

            migrationBuilder.DropForeignKey(
                name: "fk_crop_images_users",
                table: "crop_images");

            migrationBuilder.DropForeignKey(
                name: "fk_crops_plots_plot_id",
                table: "crops");

            migrationBuilder.DropForeignKey(
                name: "fk_farms_tenants_tenant_id",
                table: "farms");

            migrationBuilder.DropForeignKey(
                name: "fk_farms_users_owner_id",
                table: "farms");

            migrationBuilder.DropForeignKey(
                name: "fk_fertilization_logs_crops_crop_id",
                table: "fertilization_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_fertilization_logs_tasks_task_id",
                table: "fertilization_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_fertilization_logs_users_user_id",
                table: "fertilization_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_irrigation_logs_crops_crop_id",
                table: "irrigation_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_irrigation_logs_tasks_task_id",
                table: "irrigation_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_irrigation_logs_users_user_id",
                table: "irrigation_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_labor_logs_crops_crop_id",
                table: "labor_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_labor_logs_tasks_task_id",
                table: "labor_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_labor_logs_users_user_id",
                table: "labor_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_phenology_stages_crops_crop_id",
                table: "phenology_stages");

            migrationBuilder.DropForeignKey(
                name: "fk_phenology_stages_phenology_templates_template_id",
                table: "phenology_stages");

            migrationBuilder.DropForeignKey(
                name: "fk_phenology_stages_tenants_tenant_id",
                table: "phenology_stages");

            migrationBuilder.DropForeignKey(
                name: "fk_plots_farms_farm_id",
                table: "plots");

            migrationBuilder.DropForeignKey(
                name: "fk_sensor_devices_plots_plot_id",
                table: "sensor_devices");

            migrationBuilder.DropForeignKey(
                name: "fk_sensor_readings_sensor_devices_device_id",
                table: "sensor_readings");

            migrationBuilder.DropForeignKey(
                name: "fk_users_tenants_tenant_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "fcm_tokens");

            migrationBuilder.DropTable(
                name: "phenology_templates");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "task_occurrences");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "task_templates");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tenants",
                table: "tenants");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sensor_readings",
                table: "sensor_readings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_plots",
                table: "plots");

            migrationBuilder.DropPrimaryKey(
                name: "pk_labor_logs",
                table: "labor_logs");

            migrationBuilder.DropIndex(
                name: "ix_labor_logs_task_id",
                table: "labor_logs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_irrigation_logs",
                table: "irrigation_logs");

            migrationBuilder.DropIndex(
                name: "ix_irrigation_logs_task_id",
                table: "irrigation_logs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_fertilization_logs",
                table: "fertilization_logs");

            migrationBuilder.DropIndex(
                name: "ix_fertilization_logs_task_id",
                table: "fertilization_logs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_farms",
                table: "farms");

            migrationBuilder.DropPrimaryKey(
                name: "pk_crops",
                table: "crops");

            migrationBuilder.DropPrimaryKey(
                name: "pk_alerts",
                table: "alerts");

            migrationBuilder.DropIndex(
                name: "ix_alerts_crop_id",
                table: "alerts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sensor_devices",
                table: "sensor_devices");

            migrationBuilder.DropPrimaryKey(
                name: "pk_phenology_stages",
                table: "phenology_stages");

            migrationBuilder.DropIndex(
                name: "ix_phenology_stages_template_id",
                table: "phenology_stages");

            migrationBuilder.DropIndex(
                name: "ix_phenology_stages_tenant_id",
                table: "phenology_stages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_crop_images",
                table: "crop_images");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cost_entries",
                table: "cost_entries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_alert_rules",
                table: "alert_rules");

            migrationBuilder.DropColumn(
                name: "task_id",
                table: "labor_logs");

            migrationBuilder.DropColumn(
                name: "task_id",
                table: "irrigation_logs");

            migrationBuilder.DropColumn(
                name: "task_id",
                table: "fertilization_logs");

            migrationBuilder.DropColumn(
                name: "crop_id",
                table: "alerts");

            migrationBuilder.DropColumn(
                name: "is_custom",
                table: "phenology_stages");

            migrationBuilder.DropColumn(
                name: "stage_order",
                table: "phenology_stages");

            migrationBuilder.DropColumn(
                name: "template_id",
                table: "phenology_stages");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "phenology_stages");

            migrationBuilder.DropColumn(
                name: "ai_analyzed_at",
                table: "crop_images");

            migrationBuilder.RenameTable(
                name: "sensor_devices",
                newName: "SensorDevices");

            migrationBuilder.RenameTable(
                name: "phenology_stages",
                newName: "PhenologyStages");

            migrationBuilder.RenameTable(
                name: "crop_images",
                newName: "CropImages");

            migrationBuilder.RenameTable(
                name: "cost_entries",
                newName: "CostEntries");

            migrationBuilder.RenameTable(
                name: "alert_rules",
                newName: "AlertRules");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "users",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "last_login_at",
                table: "users",
                newName: "LastLoginAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "users",
                newName: "IsActive");

            migrationBuilder.RenameIndex(
                name: "ix_users_tenant_id_email",
                table: "users",
                newName: "IX_users_TenantId_Email");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "tenants",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "plan",
                table: "tenants",
                newName: "Plan");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tenants",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tenants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "tenants",
                newName: "IsActive");

            migrationBuilder.RenameIndex(
                name: "ix_tenants_slug",
                table: "tenants",
                newName: "IX_tenants_Slug");

            migrationBuilder.RenameColumn(
                name: "temperature",
                table: "sensor_readings",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "ph",
                table: "sensor_readings",
                newName: "Ph");

            migrationBuilder.RenameColumn(
                name: "luminosity",
                table: "sensor_readings",
                newName: "Luminosity");

            migrationBuilder.RenameColumn(
                name: "ec",
                table: "sensor_readings",
                newName: "Ec");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "sensor_readings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "rain_mm",
                table: "sensor_readings",
                newName: "RainMm");

            migrationBuilder.RenameColumn(
                name: "humidity_soil",
                table: "sensor_readings",
                newName: "HumiditySoil");

            migrationBuilder.RenameColumn(
                name: "humidity_air",
                table: "sensor_readings",
                newName: "HumidityAir");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "sensor_readings",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "ix_sensor_readings_device_id",
                table: "sensor_readings",
                newName: "IX_sensor_readings_DeviceId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "plots",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "plots",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "geojson",
                table: "plots",
                newName: "GeoJson");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "plots",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "soil_type",
                table: "plots",
                newName: "SoilType");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "plots",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "farm_id",
                table: "plots",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "area_ha",
                table: "plots",
                newName: "AreaHa");

            migrationBuilder.RenameIndex(
                name: "ix_plots_farm_id",
                table: "plots",
                newName: "IX_plots_FarmId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "labor_logs",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "labor_logs",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "labor_logs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "workers_count",
                table: "labor_logs",
                newName: "WorkersCount");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "labor_logs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "performed_at",
                table: "labor_logs",
                newName: "PerformedAt");

            migrationBuilder.RenameColumn(
                name: "hours_worked",
                table: "labor_logs",
                newName: "HoursWorked");

            migrationBuilder.RenameColumn(
                name: "crop_id",
                table: "labor_logs",
                newName: "CropId");

            migrationBuilder.RenameColumn(
                name: "activity_type",
                table: "labor_logs",
                newName: "ActivityType");

            migrationBuilder.RenameIndex(
                name: "ix_labor_logs_user_id",
                table: "labor_logs",
                newName: "IX_labor_logs_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_labor_logs_crop_id",
                table: "labor_logs",
                newName: "IX_labor_logs_CropId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "irrigation_logs",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "method",
                table: "irrigation_logs",
                newName: "Method");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "irrigation_logs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "volume_liters",
                table: "irrigation_logs",
                newName: "VolumeLiters");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "irrigation_logs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "duration_min",
                table: "irrigation_logs",
                newName: "DurationMin");

            migrationBuilder.RenameColumn(
                name: "crop_id",
                table: "irrigation_logs",
                newName: "CropId");

            migrationBuilder.RenameColumn(
                name: "applied_at",
                table: "irrigation_logs",
                newName: "AppliedAt");

            migrationBuilder.RenameIndex(
                name: "ix_irrigation_logs_user_id",
                table: "irrigation_logs",
                newName: "IX_irrigation_logs_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_irrigation_logs_crop_id",
                table: "irrigation_logs",
                newName: "IX_irrigation_logs_CropId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "fertilization_logs",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "method",
                table: "fertilization_logs",
                newName: "Method");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "fertilization_logs",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "fertilization_logs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "fertilization_logs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total_kg",
                table: "fertilization_logs",
                newName: "TotalKg");

            migrationBuilder.RenameColumn(
                name: "product_type",
                table: "fertilization_logs",
                newName: "ProductType");

            migrationBuilder.RenameColumn(
                name: "product_name",
                table: "fertilization_logs",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "next_application",
                table: "fertilization_logs",
                newName: "NextApplication");

            migrationBuilder.RenameColumn(
                name: "dose_kg_ha",
                table: "fertilization_logs",
                newName: "DoseKgHa");

            migrationBuilder.RenameColumn(
                name: "crop_id",
                table: "fertilization_logs",
                newName: "CropId");

            migrationBuilder.RenameColumn(
                name: "applied_at",
                table: "fertilization_logs",
                newName: "AppliedAt");

            migrationBuilder.RenameIndex(
                name: "ix_fertilization_logs_user_id",
                table: "fertilization_logs",
                newName: "IX_fertilization_logs_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_fertilization_logs_crop_id",
                table: "fertilization_logs",
                newName: "IX_fertilization_logs_CropId");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "farms",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "farms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lng",
                table: "farms",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "farms",
                newName: "Lat");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "farms",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "farms",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "farms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "farms",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "farms",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "farms",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "area_ha",
                table: "farms",
                newName: "AreaHa");

            migrationBuilder.RenameIndex(
                name: "ix_farms_tenant_id",
                table: "farms",
                newName: "IX_farms_TenantId");

            migrationBuilder.RenameIndex(
                name: "ix_farms_owner_id",
                table: "farms",
                newName: "IX_farms_OwnerId");

            migrationBuilder.RenameColumn(
                name: "variety",
                table: "crops",
                newName: "Variety");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "crops",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "crops",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "crops",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "yield_kg",
                table: "crops",
                newName: "YieldKg");

            migrationBuilder.RenameColumn(
                name: "plot_id",
                table: "crops",
                newName: "PlotId");

            migrationBuilder.RenameColumn(
                name: "planted_at",
                table: "crops",
                newName: "PlantedAt");

            migrationBuilder.RenameColumn(
                name: "harvested_at",
                table: "crops",
                newName: "HarvestedAt");

            migrationBuilder.RenameColumn(
                name: "estimated_harvest",
                table: "crops",
                newName: "EstimatedHarvest");

            migrationBuilder.RenameColumn(
                name: "crop_type",
                table: "crops",
                newName: "CropType");

            migrationBuilder.RenameIndex(
                name: "ix_crops_plot_id",
                table: "crops",
                newName: "IX_crops_PlotId");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "alerts",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "alerts",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "alerts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "triggered_at",
                table: "alerts",
                newName: "TriggeredAt");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "alerts",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "read_at",
                table: "alerts",
                newName: "ReadAt");

            migrationBuilder.RenameColumn(
                name: "plot_id",
                table: "alerts",
                newName: "PlotId");

            migrationBuilder.RenameColumn(
                name: "is_read",
                table: "alerts",
                newName: "IsRead");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "alerts",
                newName: "DeviceId");

            migrationBuilder.RenameColumn(
                name: "alert_type",
                table: "alerts",
                newName: "AlertType");

            migrationBuilder.RenameIndex(
                name: "ix_alerts_tenant_id",
                table: "alerts",
                newName: "IX_alerts_TenantId");

            migrationBuilder.RenameIndex(
                name: "ix_alerts_plot_id",
                table: "alerts",
                newName: "IX_alerts_PlotId");

            migrationBuilder.RenameIndex(
                name: "ix_alerts_device_id",
                table: "alerts",
                newName: "IX_alerts_DeviceId");

            migrationBuilder.RenameColumn(
                name: "lng",
                table: "SensorDevices",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "SensorDevices",
                newName: "Lat");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SensorDevices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "SensorDevices",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "plot_id",
                table: "SensorDevices",
                newName: "PlotId");

            migrationBuilder.RenameColumn(
                name: "last_seen_at",
                table: "SensorDevices",
                newName: "LastSeenAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "SensorDevices",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "firmware_ver",
                table: "SensorDevices",
                newName: "FirmwareVer");

            migrationBuilder.RenameColumn(
                name: "device_type",
                table: "SensorDevices",
                newName: "DeviceType");

            migrationBuilder.RenameColumn(
                name: "device_code",
                table: "SensorDevices",
                newName: "DeviceCode");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "SensorDevices",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "battery_pct",
                table: "SensorDevices",
                newName: "BatteryPct");

            migrationBuilder.RenameIndex(
                name: "ix_sensor_devices_plot_id",
                table: "SensorDevices",
                newName: "IX_SensorDevices_PlotId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PhenologyStages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "PhenologyStages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "started_at",
                table: "PhenologyStages",
                newName: "StartedAt");

            migrationBuilder.RenameColumn(
                name: "stage_name",
                table: "PhenologyStages",
                newName: "StageName");

            migrationBuilder.RenameColumn(
                name: "ended_at",
                table: "PhenologyStages",
                newName: "EndedAt");

            migrationBuilder.RenameColumn(
                name: "crop_id",
                table: "PhenologyStages",
                newName: "CropId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PhenologyStages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "observations",
                table: "PhenologyStages",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "ix_phenology_stages_crop_id",
                table: "PhenologyStages",
                newName: "IX_PhenologyStages_CropId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "CropImages",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "CropImages",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CropImages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "CropImages",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "taken_at",
                table: "CropImages",
                newName: "TakenAt");

            migrationBuilder.RenameColumn(
                name: "storage_key",
                table: "CropImages",
                newName: "StorageKey");

            migrationBuilder.RenameColumn(
                name: "crop_id",
                table: "CropImages",
                newName: "CropId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "CropImages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ai_diagnosis",
                table: "CropImages",
                newName: "AiDiagnosis");

            migrationBuilder.RenameColumn(
                name: "ai_confidence",
                table: "CropImages",
                newName: "AiConfidence");

            migrationBuilder.RenameIndex(
                name: "ix_crop_images_user_id",
                table: "CropImages",
                newName: "IX_CropImages_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_crop_images_crop_id",
                table: "CropImages",
                newName: "IX_CropImages_CropId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "CostEntries",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "CostEntries",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "CostEntries",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CostEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "entry_date",
                table: "CostEntries",
                newName: "EntryDate");

            migrationBuilder.RenameColumn(
                name: "crop_id",
                table: "CostEntries",
                newName: "CropId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "CostEntries",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_cost_entries_crop_id",
                table: "CostEntries",
                newName: "IX_CostEntries_CropId");

            migrationBuilder.RenameColumn(
                name: "threshold",
                table: "AlertRules",
                newName: "Threshold");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "AlertRules",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "operator",
                table: "AlertRules",
                newName: "Operator");

            migrationBuilder.RenameColumn(
                name: "metric",
                table: "AlertRules",
                newName: "Metric");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AlertRules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "AlertRules",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "plot_id",
                table: "AlertRules",
                newName: "PlotId");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "AlertRules",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "AlertRules",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_alert_rules_tenant_id",
                table: "AlertRules",
                newName: "IX_AlertRules_TenantId");

            migrationBuilder.RenameIndex(
                name: "ix_alert_rules_plot_id",
                table: "AlertRules",
                newName: "IX_AlertRules_PlotId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "labor_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "irrigation_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "fertilization_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "alerts",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "alerts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "alerts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "StageName",
                table: "PhenologyStages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "CropImages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "CropImages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StorageKey",
                table: "CropImages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<decimal>(
                name: "AiConfidence",
                table: "CropImages",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CropImages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CostEntries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AlertRules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tenants",
                table: "tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sensor_readings",
                table: "sensor_readings",
                columns: new[] { "Id", "recorded_at" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_plots",
                table: "plots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_labor_logs",
                table: "labor_logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_irrigation_logs",
                table: "irrigation_logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fertilization_logs",
                table: "fertilization_logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_farms",
                table: "farms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_crops",
                table: "crops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alerts",
                table: "alerts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SensorDevices",
                table: "SensorDevices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhenologyStages",
                table: "PhenologyStages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CropImages",
                table: "CropImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostEntries",
                table: "CostEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlertRules",
                table: "AlertRules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertRules_plots_PlotId",
                table: "AlertRules",
                column: "PlotId",
                principalTable: "plots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertRules_tenants_TenantId",
                table: "AlertRules",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_alerts_SensorDevices_DeviceId",
                table: "alerts",
                column: "DeviceId",
                principalTable: "SensorDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_alerts_plots_PlotId",
                table: "alerts",
                column: "PlotId",
                principalTable: "plots",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_alerts_tenants_TenantId",
                table: "alerts",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostEntries_crops_CropId",
                table: "CostEntries",
                column: "CropId",
                principalTable: "crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CropImages_crops_CropId",
                table: "CropImages",
                column: "CropId",
                principalTable: "crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CropImages_users_UserId",
                table: "CropImages",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_crops_plots_PlotId",
                table: "crops",
                column: "PlotId",
                principalTable: "plots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_farms_tenants_TenantId",
                table: "farms",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_farms_users_OwnerId",
                table: "farms",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fertilization_logs_crops_CropId",
                table: "fertilization_logs",
                column: "CropId",
                principalTable: "crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fertilization_logs_users_UserId",
                table: "fertilization_logs",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_irrigation_logs_crops_CropId",
                table: "irrigation_logs",
                column: "CropId",
                principalTable: "crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_irrigation_logs_users_UserId",
                table: "irrigation_logs",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_labor_logs_crops_CropId",
                table: "labor_logs",
                column: "CropId",
                principalTable: "crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_labor_logs_users_UserId",
                table: "labor_logs",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhenologyStages_crops_CropId",
                table: "PhenologyStages",
                column: "CropId",
                principalTable: "crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_plots_farms_FarmId",
                table: "plots",
                column: "FarmId",
                principalTable: "farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sensor_readings_SensorDevices_DeviceId",
                table: "sensor_readings",
                column: "DeviceId",
                principalTable: "SensorDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorDevices_plots_PlotId",
                table: "SensorDevices",
                column: "PlotId",
                principalTable: "plots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_tenants_TenantId",
                table: "users",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
