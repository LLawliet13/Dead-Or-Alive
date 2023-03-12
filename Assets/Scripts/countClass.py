import os
import re

def count_csharp_classes(directory):
    count = 0
    for filename in os.listdir(directory):
        path = os.path.join(directory, filename)
        if os.path.isdir(path):
            count += count_csharp_classes(path)
        elif filename.endswith('.cs'):
            with open(path, 'r') as f:
                contents = f.read()
                match = re.findall(r'class\s+\w+', contents)
                count += len(match)
    return count

# Lấy đường dẫn thư mục gốc
root_directory = os.getcwd()

# Đếm số lượng lớp C# trong thư mục gốc và các thư mục con của nó
count = count_csharp_classes(root_directory)

# In ra kết quả
print(f'Total C# classes in {root_directory}: {count}')
