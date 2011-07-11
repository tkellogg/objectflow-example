require 'albacore'


Albacore.configure do |config|
  
  config.fluentmigrator do |migrator|
    migrator.provider = "sqlserver2008"
    migrator.command = "packages/FluentMigrator.0.9.1.0/tools/Migrate.exe"
    dll = 'objectflow_example.Migrations'
    migrator.target = "#{dll}/bin/Debug/#{dll}.dll"
    migrator.connection = 'Server=.\MSSQL2008R2; Database=objectflow; User Id=objectflow; Password=password'
  end

end


namespace :db do
  
  task :migrate => 'migrate:up'
  
  namespace :migrate do
    
    fluentmigrator :up do |mig|
      mig.task = "up"
    end
    
    fluentmigrator :down do |mig|
      mig.task = "down"
    end
    
  end
  
end
