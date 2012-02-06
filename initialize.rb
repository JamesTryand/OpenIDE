#!/usr/bin/env ruby
require 'rbconfig'

WINDOWS = RbConfig::CONFIG['host_os'] =~ /mswin|mingw/

working_directory = ARGV[0]
default_language = ""
if ARGV.length > 1
	default_language = ARGV[1]
end
enabled_languages = ""
if ARGV.length > 2
	enabled_languages = ARGV[2]
end

t1 = Thread.new do
	if WINDOWS
		%x[CodeEngine/OpenIDE.CodeEngine.exe "#{working_directory}" "#{default_language}" "#{enabled_languages}"]
	else
		%x[mono ./CodeEngine/OpenIDE.CodeEngine.exe "#{working_directory}" "#{default_language}" "#{enabled_languages}"]
	end
end

sleep 5
t1.kill

