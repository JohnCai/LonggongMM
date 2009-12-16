DOT_NET_PATH = "C:/Windows/Microsoft.NET/Framework/v3.5/"
NUNIT_PATH = "tools//nunit-2.4.6//nunit-console.exe"
PACKAGE_PATH = "build/package"
SOLUTION = "src/XF.sln"
CONFIG = "Debug"

task :default => ["build:all"]

namespace :build do
  task :all => [:compile, :harvest, :test]
  
  task :compile do
    sh "#{DOT_NET_PATH}msbuild.exe /p:Configuration=#{CONFIG} #{SOLUTION}"
  end
  
  task :harvest => [:compile] do
    require 'build/scripts/file_handling.rb'
    package_files
  end
end