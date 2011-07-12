require 'albacore'


Albacore.configure do |config|
  
  config.fluentmigrator do |migrator|
    migrator.provider = "sqlserver2008"
    migrator.command = "packages/FluentMigrator.0.9.1.0/tools/Migrate.exe"
    dll = 'objectflow_example.Migrations'
    migrator.target = "#{dll}/bin/Debug/#{dll}.dll"
    migrator.connection = 'Data Source=localhost\MSSQL2008R2; Initial Catalog=objectflow; User Id=objectflow; Password=password; Connection Timeout=60; Persist Security Info=True;'
    migrator.script_directory = "objectflow_example.Migrations"
  end

end

desc "compile the project and setup the database"
task :default => [:build, 'db:migrate']

namespace :db do
  
  desc "setup the database properly"
  task :migrate => 'migrate:up'
  
  namespace :migrate do
    
    fluentmigrator :up do |mig|
      mig.task = "migrate:up"
    end
    
    fluentmigrator :down do |mig|
      mig.task = "migrate:down"
    end
    
    fluentmigrator :rollback do |mig|
      mig.task = "rollback"
    end
    
  end
  
  desc "reset the database by migrating down and back up"
  task :reset => ['migrate:down', 'migrate:up']
  
end

desc "compile the project"
task :build => 'build:main'

namespace :build do

  msbuild :objectflow do |msb|
    msb.solution = "objectflow/ObjectWorkFlow.sln"  
  end
  
  msbuild :main => :objectflow do |msb|
    msb.solution = "objectflow-example.sln"
  end
  
end



