class AttendanceRecord < ApplicationRecord
  belongs_to :activity
  belongs_to :student
end
