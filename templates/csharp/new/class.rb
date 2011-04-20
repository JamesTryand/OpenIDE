#!/usr/bin/env ruby

if ARGV[0] == 'get_file_extension'
	puts '.cs'
	exit
end

classname = ARGV[0]
namespace = ARGV[1]
parameterfile = ARGV[2]

puts "using System;

namespace #{namespace}
{
	class #{classname}
	{
	}
}"